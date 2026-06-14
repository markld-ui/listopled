# Dev Setup

Этот файл описывает целевую локальную разработку. Команды будут уточнены после создания backend solution, Nuxt app и Docker Compose.

## Предварительные требования

- .NET 10 SDK.
- Node.js LTS, совместимый с Nuxt 3.
- Docker Desktop или Docker Engine.
- Git.

## .NET SDK

В корне проекта есть `global.json`, который фиксирует SDK:

```text
10.0.201
```

Локально также может быть установлен preview SDK, но проект должен собираться через зафиксированный SDK, если он доступен.

## Планируемый запуск

Полный dev stack:

```bash
docker compose up -d
```

Backend отдельно:

```bash
cd backend
dotnet restore
dotnet test
dotnet run --project src/Listopled.Api
```

Frontend отдельно:

```bash
cd frontend
npm install
npm run dev
```

## Dev admin

```text
email: admin@listopled.local
password: Admin123!
```

Этот пароль только для локальной разработки.

## Environment

`.env.example` должен содержать переменные для:

- environment;
- database;
- JWT;
- admin seed;
- CORS;
- internal API;
- storage;
- cache;
- analytics;
- SMTP.

Backend persistence использует PostgreSQL connection string из `ConnectionStrings:DefaultConnection`. Если она не задана или равна placeholder `ConfiguredByEnvironment`, `Listopled.Infrastructure` собирает connection string из `DB_HOST`, `DB_PORT`, `DB_NAME`, `DB_USER`, `DB_PASSWORD`.

Реальные секреты не должны попадать в `appsettings*.json` или git. Для production используются environment variables / secret storage.

## Документация API

В Development OpenAPI UI доступен. В Production недоступен и должен возвращать `404`.

Для backend используется Swashbuckle. Scalar и NSwag пока не подключаются.
