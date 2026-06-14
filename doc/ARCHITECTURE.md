# Architecture

## Общий принцип

Backend является source of truth для бизнес-логики. Frontend и BFF не имеют права финально считать цену, определять права пользователя или доверять данным клиента без backend-проверки.

## Production traffic

```text
Browser
  ↓
Nginx
  ↓
Nuxt frontend / BFF
  ↓ internal Docker network
ASP.NET Core API
  ↓
PostgreSQL / Valkey / MinIO
```

Правила:

- браузер не ходит напрямую в ASP.NET API;
- браузер ходит в Nuxt server routes / BFF;
- BFF выполняет базовую валидацию и проксирует запросы во внутренний API;
- ASP.NET API выполняет полную авторизацию, валидацию и бизнес-логику;
- `/swagger`, `/scalar`, `/openapi` в production возвращают 404;
- неизвестные routes возвращают 404.

## Backend

Используется Clean Architecture / modular monolith.

Проекты:

- `Listopled.Api`;
- `Listopled.Application`;
- `Listopled.Domain`;
- `Listopled.Infrastructure`;
- `Listopled.UnitTests`;
- `Listopled.IntegrationTests`.

`Listopled.Domain` содержит entities, value objects, enums, domain services и доменные правила без зависимостей от EF и ASP.NET.

`Listopled.Application` содержит CQRS commands, queries, handlers, DTO, validators, AutoMapper profiles и интерфейсы:

- `IApplicationDbContext`;
- `ICurrentUserService`;
- `ICacheService`;
- `IFileStorageService`;
- `IEmailService`;
- `IPaymentProvider`;
- `IAnalyticsService`.

`Listopled.Infrastructure` содержит EF Core DbContext, configurations, migrations, seed data, Valkey cache, MinIO/local storage, SMTP, payment stub и analytics persistence.

`Listopled.Api` содержит controllers, middleware, auth, OpenAPI config, exception handling, rate limiting, security headers, health checks и `Program.cs`.

Используются Controllers, не Minimal APIs.

## Frontend

Nuxt 3 структура:

```text
frontend/src/
├── assets/
├── components/
│   ├── ui/
│   ├── public/
│   └── admin/
├── composables/
├── layouts/
├── middleware/
├── pages/
├── server/
│   └── api/
├── stores/
├── types/
└── utils/
```

Nuxt server routes являются BFF-слоем. Они нужны для скрытия backend API, SSR, same-domain UX, работы с httpOnly cookies и уменьшения прямого сканирования ASP.NET endpoints.

## Phase 2 public conversion slice

Фаза 2 строится как `Public Landing + Calculator Conversion Slice`, а не как отдельный backend-калькулятор.

Архитектурный поток Фазы 2:

```text
Публичная главная страница Nuxt
  ↓
Nuxt BFF routes
  ↓
ASP.NET public API
  ↓
PostgreSQL seed/config data
```

В этот срез входят:

- backend инфраструктура для DI и будущих CQRS handlers;
- доменная логика расчета цены на backend;
- public API для настроек сайта, контактов, галереи, отзывов, FAQ и калькулятора;
- Nuxt BFF routes, которые не добавляют бизнес-логику;
- frontend foundation и skeleton лендинга;
- frontend calculator component как часть публичного conversion flow;
- CTA helper для перехода в мессенджеры после расчета.

Backend остается source of truth для цены и бизнес-правил. Frontend может показывать результат, подсказки и CTA, но не становится источником истины для расчета.

## Кэш и файлы

Кэш через `ICacheService` не является обязательным для бизнес-корректности. Если Valkey недоступен, система работает с БД.

Файлы идут через `IFileStorageService`. Реализации: `LocalFileStorageService` для экономного MVP и MinIO/S3-compatible storage для production или расширения.
