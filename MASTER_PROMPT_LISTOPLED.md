# MASTER_PROMPT.md — LISTOPLED

## Роль Codex

Ты работаешь как **senior product manager + UX/CRO-аналитик + solution architect + senior .NET backend developer + Nuxt frontend developer + UI/UX designer + технический писатель**.

Твоя задача — спроектировать и постепенно реализовать fullstack-проект для бренда **«ЛИСТОПЛЕД»**: сайт авторских пледов ручной работы с красивой публичной частью, калькулятором стоимости, формой обращения, мессенджерами, кастомной админ-панелью, метриками и документацией.

Этот файл является **главным источником истины**. При каждом новом сеансе работы сначала перечитай его и файлы из `doc/`. Любое решение, противоречащее этому документу, сначала фиксируется в документации и только потом реализуется в коде.

---

## 1. Контекст проекта

У владельца бренда уже есть лендинг на Tilda:

```text
https://listopled.tilda.ws/
```

Бренд: **ЛИСТОПЛЕД**.  
Мастер: Анна Слинькова.  
Продукт: авторские пледы в форме листьев, ручная работа, индивидуальное изготовление.

Новый проект создаётся не как «игрушечный сайт», а как реальный коммерческий инструмент для маленького handmade-бренда. У мастера небольшой объём заказов, поэтому проект должен быть красивым, удобным, безопасным, но недорогим в эксплуатации и не прожорливым по ресурсам.

---

## 2. Главная продуктовая идея

Это **не маркетплейс** и не полноценный интернет-магазин в MVP.

Это **productized landing**:

```text
Клиент увидел работы → понял ценность → рассчитал примерную цену → написал мастеру.
```

Текущий реальный процесс заказа:

1. Клиент смотрит работы.
2. Считает примерную стоимость в калькуляторе.
3. Находит контакты: Telegram / WhatsApp / VK.
4. Пишет мастеру напрямую.
5. В личной переписке обсуждаются:
   - идея;
   - размер;
   - ткань;
   - форма листа;
   - цвет;
   - финальная цена;
   - сроки;
   - доставка;
   - оплата.

MVP должен **оцифровать и улучшить этот путь**, но не ломать его.

---

## 3. Бизнес-цели

Сайт должен:

- усиливать личный бренд мастера;
- повышать доверие к handmade-продукту;
- красиво показывать работы;
- объяснять ценность ручной работы;
- помогать клиенту быстро понять примерную цену;
- переводить клиента в личное общение с мастером;
- снижать количество однотипных вопросов;
- давать мастеру удобную админку;
- показывать бизнес-метрики для анализа и улучшения продаж.

Ключевые KPI:

- количество посещений сайта;
- количество кликов в WhatsApp / Telegram / VK;
- количество начатых и завершённых расчётов;
- количество отправленных заявок;
- конверсия из расчёта в обращение;
- популярные размеры, ткани и формы листьев;
- самые просматриваемые работы;
- динамика обращений по дням;
- статусы обращений;
- доля завершённых обращений.

---

## 4. MVP и Post-MVP

### 4.1 Входит в MVP

- публичный лендинг;
- галерея / витрина работ;
- калькулятор стоимости;
- форма обращения;
- переходы в WhatsApp / Telegram / VK;
- генерация текста для WhatsApp после расчёта;
- кастомная админ-панель;
- управление обращениями;
- управление калькулятором;
- управление галереей;
- управление отзывами;
- управление FAQ;
- управление контактами;
- визуализация бизнес-метрик в админке;
- лёгкая внутренняя аналитика;
- dev и production среды;
- reverse proxy;
- backend API скрыт от прямого доступа пользователя;
- OpenAPI-документация в dev;
- Docker Compose;
- документация в `doc/`;
- unit tests для бизнес-логики.

### 4.2 Не входит в MVP

Не реализовывать в MVP:

- онлайн-оплату;
- корзину;
- личный кабинет клиента;
- регистрацию клиентов;
- wishlist;
- историю заказов для клиента;
- полноценную CRM;
- микросервисы;
- Kafka;
- RabbitMQ;
- сложные промокоды;
- WhatsApp Business API;
- мобильное приложение.

### 4.3 Post-MVP

Оставить архитектурное пространство под:

- онлайн-оплату;
- личный кабинет;
- статусы заказа для клиента;
- уведомления;
- полноценную CRM;
- блог;
- SEO-страницы;
- промокоды;
- интеграции с Telegram/WhatsApp;
- CRM-воронку;
- расширенную аналитику.

Для будущей оплаты создать интерфейс `IPaymentProvider` с заглушкой, но не реализовывать оплату.

---

## 5. Технологический стек

### 5.1 Backend

Использовать:

- **.NET 10**;
- **ASP.NET Core 10 Web API**;
- C# актуальной версии для .NET 10;
- PostgreSQL 16;
- Entity Framework Core;
- CQRS + MediatR;
- FluentValidation;
- **AutoMapper**;
- JWT для админки;
- Serilog;
- OpenAPI;
- xUnit;
- WebApplicationFactory / Testcontainers по необходимости;
- Docker.

Важно:

- Если какой-либо NuGet-пакет плохо совместим с .NET 10, зафиксировать проблему в `doc/ASSUMPTIONS.md`.
- Не откатываться на .NET 8 без явного обоснования.
- Не вводить платные зависимости.
- Все библиотеки должны быть бесплатными или open-source.

### 5.2 API-документация

Нужно генерировать OpenAPI-спецификацию.

Приоритет:

1. Swagger / Swashbuckle, если стабильно работает с .NET 10.
2. Scalar, если он лучше подходит под .NET 10 или Swagger вызывает проблемы.
3. NSwag как fallback.

Правило:

- В `Development` документация API доступна.
- В `Production` Swagger / Scalar / OpenAPI UI недоступны снаружи и должны отдавать `404`.

### 5.3 Frontend

Финальный выбор frontend:

- **Nuxt 3**;
- Vue 3;
- TypeScript;
- Vite;
- Pinia;
- Tailwind CSS;
- CSS variables;
- VeeValidate + Zod;
- Chart.js / vue-chartjs для графиков;
- Nuxt `useFetch` / `$fetch`;
- Nuxt server routes как BFF-слой.

Почему Nuxt 3:

- нужен SSR для SEO;
- нужен backend-for-frontend слой, чтобы не раскрывать ASP.NET API напрямую браузеру;
- можно сделать публичные страницы SEO-дружелюбными;
- админку можно делать client-side;
- Vue/Nuxt достаточно лаконичны для небольшого проекта.

### 5.4 Кэш

Использовать:

- Valkey как бесплатный open-source Redis-compatible сервер;
- интерфейс `ICacheService`;
- кэшировать только то, что действительно полезно.

Кэш не должен быть обязательным для бизнес-корректности. Если Valkey недоступен, система должна деградировать и работать с БД.

### 5.5 S3-хранилище

Использовать S3-compatible storage через интерфейс `IFileStorageService`.

Для self-hosted варианта:

- MinIO;
- бесплатно;
- запускается в Docker;
- подходит как S3-совместимое хранилище.

Для экономного MVP допускается начать с `LocalFileStorageService`, если MinIO слишком тяжёл для минимального VPS, но архитектура должна иметь `IFileStorageService`, чтобы позже переключиться на MinIO/S3 без переписывания бизнес-логики.

Решение о фактическом включении MinIO в production должно быть отражено в `doc/DEPLOYMENT.md`.

### 5.6 Аналитика

Обязательно реализовать:

1. Внутренние бизнес-метрики в собственной БД.
2. Админскую страницу аналитики с графиками.
3. Опциональную интеграцию с self-hosted Umami через Docker profile `analytics`.

Umami не должен быть жёсткой обязательной зависимостью для работы сайта.

---

## 6. Бесплатность и минимальные расходы

Проект должен использовать только бесплатные технологии:

- open-source библиотеки;
- self-hosted решения;
- бесплатные инструменты;
- без платных SaaS, если есть нормальная бесплатная альтернатива.

Платные хостинги/VPS не учитывать, но проект должен быть рассчитан на минимальную конфигурацию VPS.

Целевая конфигурация:

```text
CPU: 1–2 vCPU
RAM: 1–2 GB
Disk: 20 GB SSD
```

Правила ресурсной эффективности:

- не делать тяжёлые фоновые процессы без необходимости;
- не запускать лишние контейнеры в production без причины;
- использовать Docker Compose profiles;
- Umami вынести в optional profile;
- MinIO можно вынести в optional profile, если используется local storage;
- ограничить память контейнеров;
- включить ротацию логов;
- PostgreSQL настроить под малый VPS;
- Valkey ограничить по памяти;
- все списки отдавать с пагинацией;
- не делать запросов без LIMIT в админке и публичной части.

---

## 7. Среды

Должны быть две среды:

- `Development`;
- `Production`.

### 7.1 Development

В dev:

- API docs доступны;
- frontend hot reload;
- backend logs Debug/Information;
- PostgreSQL можно открыть наружу на `localhost:5432`;
- Valkey можно открыть наружу на `localhost:6379`;
- MinIO console доступна, если profile включён;
- CORS разрешает localhost;
- seed создаёт dev admin;
- можно запускать backend и frontend отдельно.

### 7.2 Production

В production:

- Swagger / Scalar / OpenAPI UI недоступны;
- backend API недоступен напрямую из браузера;
- БД, Valkey, MinIO не имеют публичных портов;
- HTTPS через nginx / certbot / reverse proxy;
- логи Information/Warning;
- ротация логов;
- секреты только через env;
- CORS максимально строгий;
- ошибки не раскрывают stack trace;
- `/admin` защищён авторизацией;
- неизвестные API routes возвращают 404.

---

## 8. Структура проекта

Создать такую структуру:

```text
listopled/
├── MASTER_PROMPT.md
├── docker-compose.yml
├── docker-compose.override.yml
├── docker-compose.prod.yml
├── .env.example
├── .gitignore
│
├── nginx/
│   ├── nginx.dev.conf
│   └── nginx.prod.conf
│
├── static/
│   └── photos/
│
├── doc/
│   ├── README.md
│   ├── PRODUCT_VISION.md
│   ├── MARKET_AND_CRO_ANALYSIS.md
│   ├── TECHNICAL_SPECIFICATION.md
│   ├── ARCHITECTURE.md
│   ├── DOMAIN.md
│   ├── DATABASE_SCHEMA.md
│   ├── API.md
│   ├── UI_UX_GUIDE.md
│   ├── ADMIN_UX_GUIDE.md
│   ├── ANALYTICS.md
│   ├── SECURITY_AND_PRIVACY.md
│   ├── DEPLOYMENT.md
│   ├── DEV_SETUP.md
│   ├── DEVELOPMENT_RULES.md
│   ├── ASSUMPTIONS.md
│   └── CHANGELOG.md
│
├── backend/
│   ├── Listopled.sln
│   ├── src/
│   │   ├── Listopled.Api/
│   │   ├── Listopled.Application/
│   │   ├── Listopled.Domain/
│   │   └── Listopled.Infrastructure/
│   └── tests/
│       ├── Listopled.UnitTests/
│       └── Listopled.IntegrationTests/
│
└── frontend/
    ├── nuxt.config.ts
    ├── package.json
    ├── Dockerfile
    └── src/
        ├── assets/
        ├── components/
        │   ├── ui/
        │   ├── layout/
        │   ├── public/
        │   └── admin/
        ├── composables/
        ├── pages/
        │   ├── index.vue
        │   ├── gallery.vue
        │   ├── calculator.vue
        │   ├── about.vue
        │   ├── reviews.vue
        │   ├── privacy.vue
        │   ├── admin/
        │   └── auth/
        ├── server/
        │   └── api/
        ├── stores/
        ├── types/
        └── utils/
```

---

## 9. Статические материалы бренда

Все брендовые фотографии, логотипы, эскизы, изображения и будущие материалы из Figma находятся по пути:

```text
listopled/static/photos
```

Правила:

- при первичной верстке использовать материалы из `static/photos`;
- не хардкодить абсолютные пути;
- предусмотреть, что позже туда добавятся материалы из Figma;
- дизайн должен легко обновляться после появления финальных цветов, шрифтов и макетов;
- все временные дизайнерские решения фиксировать в `doc/ASSUMPTIONS.md`.

---

## 10. Дизайн и визуальный стиль

Дизайн должен быть вдохновлён текущим сайтом на Tilda и материалами мастера, но не копировать Tilda один в один.

Ассоциации:

- ручная работа;
- тепло;
- уют;
- природа;
- листья;
- мягкость;
- индивидуальность;
- доверие;
- минималистичная роскошь;
- спокойная женственность без приторности.

Нельзя делать:

- безликий e-commerce;
- белый фон + синие кнопки;
- типовой Bootstrap-like интерфейс;
- перегруженный маркетплейс;
- агрессивные продажи.

Placeholder-палитра до Figma:

```css
--color-cream: #F5EFE4;
--color-linen: #E8DCCB;
--color-mocha: #8B6A52;
--color-bark: #5C4033;
--color-sand: #C8AD96;
--color-white: #FDFAF6;
--color-text: #3A2A1E;
--color-muted: #9A7E6F;
--color-leaf: #7A8F65;
--color-soft-green: #DDE6D2;
```

Правила:

- все цвета через CSS variables;
- не хардкодить цвета прямо в компонентах;
- Tailwind должен использовать CSS variables;
- шрифты должны быть бесплатными;
- желательно self-host fonts или системный fallback;
- фото должны быть ключевым элементом продаж.

---

## 11. Локализация

Вся система только на русском языке:

- публичный сайт;
- админка;
- ошибки;
- валидация;
- подсказки;
- onboarding;
- графики;
- пустые состояния;
- письма;
- README для мастера.

Тон:

- простой;
- спокойный;
- дружелюбный;
- без IT-жаргона.

Примеры:

Плохо:

```text
Validation error
Submit
Entity updated
Pricing rules
```

Хорошо:

```text
Проверьте, пожалуйста, номер телефона
Отправить заявку
Изменения сохранены
Настройки калькулятора
```

---

## 12. Адаптивность

Поддерживаемые браузеры:

- Chrome;
- Яндекс Браузер;
- Firefox;
- Safari macOS;
- Safari iOS;
- Android Chrome.

Устройства:

- ноутбуки;
- desktop;
- iOS;
- Android;
- планшеты.

Контрольные ширины:

```text
360px
375px
390px
430px
768px
1024px
1366px
1440px
```

Требования:

- mobile-first;
- tap target минимум 44x44;
- калькулятор удобен на телефоне;
- формы не мелкие;
- CTA всегда заметен;
- админка удобна минимум на ноутбуке и планшете;
- публичная часть удобна на телефоне;
- изображения оптимизированы;
- layout не ломается на Safari iOS.

---

## 13. Архитектура доступа и reverse proxy

В production backend API не должен быть напрямую доступен обычному пользователю.

Финальная схема:

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

Правило:

- браузер НЕ ходит напрямую в ASP.NET API;
- браузер ходит в Nuxt server routes / BFF;
- Nuxt server routes валидируют входные данные на базовом уровне и проксируют запросы в ASP.NET API по внутренней Docker-сети;
- ASP.NET API всё равно выполняет полную авторизацию, валидацию и бизнес-логику;
- публичный backend route не должен быть единственным барьером безопасности;
- `/swagger`, `/scalar`, `/openapi` в production возвращают 404;
- неизвестные маршруты возвращают 404.

В dev можно открыть backend порт для разработки.

---

## 14. Backend architecture

Использовать Clean Architecture / modular monolith.

### 14.1 Projects

```text
Listopled.Api
Listopled.Application
Listopled.Domain
Listopled.Infrastructure
Listopled.UnitTests
Listopled.IntegrationTests
```

### 14.2 Listopled.Domain

Содержит:

- entities;
- value objects;
- enums;
- domain services;
- доменные правила;
- без зависимостей от EF, ASP.NET, NuGet инфраструктуры.

### 14.3 Listopled.Application

Содержит:

- CQRS commands;
- queries;
- handlers;
- DTO;
- validators;
- AutoMapper profiles;
- interfaces:
  - `IApplicationDbContext`;
  - `ICurrentUserService`;
  - `ICacheService`;
  - `IFileStorageService`;
  - `IEmailService`;
  - `IPaymentProvider`;
  - `IAnalyticsService`.

### 14.4 Listopled.Infrastructure

Содержит:

- EF Core DbContext;
- configurations;
- migrations;
- seed data;
- Valkey cache implementation;
- MinIO/local storage implementation;
- SMTP implementation;
- payment provider stub;
- analytics persistence.

### 14.5 Listopled.Api

Содержит:

- controllers;
- middleware;
- auth;
- OpenAPI/Swagger/Scalar config;
- exception handling;
- rate limiting;
- security headers;
- health checks;
- Program.cs.

Использовать Controllers, не Minimal APIs.

---

## 15. Frontend architecture

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
│   ├── default.vue
│   └── admin.vue
├── middleware/
│   └── auth.ts
├── pages/
├── server/
│   └── api/
├── stores/
├── types/
└── utils/
```

### 15.1 Nuxt BFF

Использовать Nuxt server routes как BFF:

```text
/frontend/src/server/api/public/calculator/options.get.ts
/frontend/src/server/api/public/calculator/quote.post.ts
/frontend/src/server/api/public/leads.post.ts
/frontend/src/server/api/admin/leads.get.ts
...
```

BFF делает запросы к внутреннему ASP.NET API:

```text
http://api:8080/api/v1/...
```

BFF не является source of truth. Он нужен для:

- скрытия backend API от браузера;
- SSR;
- same-domain UX;
- удобной работы с httpOnly cookies;
- защиты от прямого сканирования ASP.NET endpoints.

---

## 16. Публичный сайт

Главная страница:

1. Header.
2. Hero.
3. О мастере.
4. Что такое ЛИСТОПЛЕД.
5. Галерея работ.
6. Преимущества.
7. Калькулятор.
8. Как происходит заказ.
9. Материалы и размеры.
10. Отзывы.
11. FAQ.
12. Доставка и оплата.
13. Контакты.
14. Footer.

Главная цель:

```text
Посетитель должен захотеть написать мастеру.
```

CTA:

- «Рассчитать стоимость»;
- «Посмотреть работы»;
- «Написать мастеру»;
- «Хочу похожий плед»;
- «Обсудить идею».

---

## 17. Калькулятор стоимости

### 17.1 Source of truth

Цена считается только на backend.

Frontend и BFF не имеют права быть source of truth для цены.

Запрещено:

- финально рассчитывать цену на frontend;
- доверять цене, пришедшей с frontend;
- сохранять заявку с ценой без пересчёта на backend.

### 17.2 Формула

Текущая формула:

```text
Итого = F + T * R + B - I
```

Где:

- `R` — коэффициент размера;
- `T` — стоимость ткани;
- `F` — доплата за форму листа;
- `B` — базовая фиксированная часть;
- `I` — скидка.

Начальное значение:

```text
B = 4000
```

### 17.3 Seed: размеры

```text
Взрослый 150х220 см = 5
Подростковый 150х170 см = 4
Детский 100х150 см = 2
Для новорожденных 80х100 см = 1.6
```

### 17.4 Seed: ткани

```text
Габардин = 650
Флис = 700
Трикотаж = 1000
Бархат = 950
Хлопок = 1200
Лен = 1200
Плащевка / Габардин для пикника = 700
```

### 17.5 Seed: формы листьев

Формы листьев должны быть управляемыми из админки.

Примеры:

```text
Дуб = 300
Дуб монгольский = 300
Дуб северный = 500
Каштан = 500
Клен = 500
Клевер = 300
Мелисса = 300
```

Если на текущем сайте есть полный список — перенести его в seed data.

### 17.6 Seed: скидки

Пример:

```text
Я новый подписчик Instagram = 300
```

Скидки должны быть управляемыми из админки.

### 17.7 Обязательный unit-test

```text
Размер: Взрослый 150х220, R = 5
Ткань: Флис, T = 700
Форма: Дуб, F = 300
База: B = 4000
Скидка: I = 300

Итого = 300 + 700 * 5 + 4000 - 300 = 7500 ₽
```

Этот тест обязателен.

### 17.8 UX калькулятора

Калькулятор должен:

- быть простым;
- быть пошаговым или визуально понятным;
- показывать расшифровку цены;
- объяснять, что цена предварительная;
- после расчёта предлагать написать мастеру;
- формировать текст для WhatsApp.

Пример WhatsApp текста:

```text
Здравствуйте! Хочу обсудить плед ЛИСТОПЛЕД.

Параметры:
Размер: Взрослый 150х220 см
Ткань: Флис
Форма листа: Дуб
Предварительная цена: 7500 ₽

Хочу уточнить детали, сроки и доставку.
```

---

## 18. Обращения

В MVP использовать термин **«обращение»** или **«заявка»**, а не «заказ».

### 18.1 Entity: Inquiry

```csharp
public class Inquiry : BaseEntity
{
    public string InquiryNumber { get; private set; }
    public string CustomerName { get; private set; }
    public string? Phone { get; private set; }
    public string? WhatsApp { get; private set; }
    public string? Telegram { get; private set; }
    public string? Vk { get; private set; }
    public string? Email { get; private set; }

    public Guid? BlanketSizeId { get; private set; }
    public Guid? FabricId { get; private set; }
    public Guid? LeafShapeId { get; private set; }
    public Guid? DiscountId { get; private set; }

    public decimal? EstimatedPrice { get; private set; }
    public string? CustomerComment { get; private set; }
    public PreferredContactMethod PreferredContactMethod { get; private set; }
    public InquiryStatus Status { get; private set; }
    public string? AdminNote { get; private set; }

    public bool PersonalDataConsent { get; private set; }
    public DateTime? ContactedAt { get; private set; }
}
```

### 18.2 Status enum

```csharp
public enum InquiryStatus
{
    New,
    Seen,
    Contacted,
    InDiscussion,
    Accepted,
    Rejected,
    Completed
}
```

В UI:

```text
New = Новая
Seen = Просмотрена
Contacted = Связались
InDiscussion = Обсуждение
Accepted = Принята
Rejected = Отклонена
Completed = Завершена
```

---

## 19. Доменная модель MVP

Минимальные сущности:

- `AdminUser`;
- `Inquiry`;
- `BlanketSize`;
- `Fabric`;
- `LeafShape`;
- `Discount`;
- `PriceCalculationSettings`;
- `PriceQuote`;
- `GalleryItem`;
- `Testimonial`;
- `FaqItem`;
- `ContactSettings`;
- `SiteSettings`;
- `AnalyticsEvent`;
- `AdminMetricSnapshot` при необходимости.

### 19.1 AdminUser

```csharp
public class AdminUser : BaseEntity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string DisplayName { get; private set; }
    public bool IsActive { get; private set; }
    public string? RefreshTokenHash { get; private set; }
    public DateTime? RefreshTokenExpiresAt { get; private set; }
    public bool HasCompletedOnboarding { get; private set; }
    public DateTime? OnboardingCompletedAt { get; private set; }
}
```

### 19.2 BlanketSize

```csharp
public class BlanketSize : BaseEntity
{
    public string Name { get; private set; }
    public string Dimensions { get; private set; }
    public decimal Coefficient { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
}
```

### 19.3 Fabric

```csharp
public class Fabric : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal PricePerUnit { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
}
```

### 19.4 LeafShape

```csharp
public class LeafShape : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Surcharge { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
}
```

### 19.5 Discount

```csharp
public class Discount : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Amount { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
}
```

### 19.6 GalleryItem

```csharp
public class GalleryItem : BaseEntity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string ImageUrl { get; private set; }
    public string AltText { get; private set; }
    public bool IsPublished { get; private set; }
    public bool IsFeatured { get; private set; }
    public int SortOrder { get; private set; }
}
```

### 19.7 AnalyticsEvent

```csharp
public class AnalyticsEvent : BaseEntity
{
    public string EventName { get; private set; }
    public string? PayloadJson { get; private set; }
    public string? SessionId { get; private set; }
    public string? Path { get; private set; }
    public string? Referrer { get; private set; }
    public string? UserAgentHash { get; private set; }
    public DateTime OccurredAt { get; private set; }
}
```

---

## 20. API

Backend internal base URL:

```text
http://api:8080/api/v1
```

Browser-facing Nuxt BFF routes:

```text
/api/public/...
/api/admin/...
```

ASP.NET API в production не должен быть напрямую доступен извне.

### 20.1 Общий формат ответа

```json
{
  "success": true,
  "data": {},
  "error": null,
  "meta": null
}
```

Ошибка:

```json
{
  "success": false,
  "data": null,
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Проверьте корректность данных",
    "details": [
      {
        "field": "phone",
        "message": "Введите корректный номер телефона"
      }
    ]
  },
  "meta": null
}
```

### 20.2 Public backend endpoints

```text
GET    /api/v1/public/site-settings
GET    /api/v1/public/contact-settings
GET    /api/v1/public/gallery
GET    /api/v1/public/testimonials
GET    /api/v1/public/faq
GET    /api/v1/public/calculator/options
POST   /api/v1/public/calculator/quote
POST   /api/v1/public/inquiries
POST   /api/v1/public/analytics/events
GET    /api/v1/public/sitemap.xml
```

### 20.3 Admin backend endpoints

```text
POST   /api/v1/admin/auth/login
POST   /api/v1/admin/auth/refresh
POST   /api/v1/admin/auth/logout
GET    /api/v1/admin/auth/me

GET    /api/v1/admin/dashboard
GET    /api/v1/admin/analytics/dashboard

GET    /api/v1/admin/inquiries
GET    /api/v1/admin/inquiries/{id}
PATCH  /api/v1/admin/inquiries/{id}/status
PATCH  /api/v1/admin/inquiries/{id}/note

GET    /api/v1/admin/calculator/options
PUT    /api/v1/admin/calculator/base-amount

CRUD   /api/v1/admin/blanket-sizes
CRUD   /api/v1/admin/fabrics
CRUD   /api/v1/admin/leaf-shapes
CRUD   /api/v1/admin/discounts

CRUD   /api/v1/admin/gallery
CRUD   /api/v1/admin/testimonials
CRUD   /api/v1/admin/faq

GET    /api/v1/admin/contact-settings
PUT    /api/v1/admin/contact-settings

GET    /api/v1/admin/site-settings
PUT    /api/v1/admin/site-settings

GET    /api/v1/admin/onboarding/status
POST   /api/v1/admin/onboarding/complete
```

### 20.4 Не создавать в MVP

```text
/api/v1/auth/register
/api/v1/users/me
/api/v1/cart
/api/v1/wishlist
/api/v1/payments
/api/v1/customer/orders
```

---

## 21. CQRS

Примерная структура Application:

```text
Application/
├── Common/
│   ├── Behaviors/
│   │   ├── ValidationBehavior.cs
│   │   ├── LoggingBehavior.cs
│   │   └── TransactionBehavior.cs
│   ├── Interfaces/
│   ├── Mappings/
│   │   └── MappingProfile.cs
│   ├── Models/
│   └── Exceptions/
│
├── Calculator/
│   ├── Queries/
│   └── Commands/
├── Inquiries/
│   ├── Queries/
│   └── Commands/
├── Gallery/
├── Testimonials/
├── Faq/
├── Settings/
├── Analytics/
└── Auth/
```

AutoMapper profiles лежат в Application layer.

---

## 22. Админ-панель

Целевая аудитория админки:

- женщина 40–45 лет;
- не технический специалист;
- уверенно пользуется браузером и мессенджерами;
- должна сама менять фото, цены, отзывы, FAQ и статусы обращений.

Главный принцип:

```text
Админка должна ощущаться как понятный рабочий кабинет мастера, а не как панель программиста.
```

### 22.1 Разделы

- Главная;
- Обращения;
- Галерея;
- Калькулятор;
- Отзывы;
- FAQ;
- Контакты;
- Аналитика;
- Настройки;
- Помощь.

### 22.2 UX-правила

- крупные кнопки;
- иконка всегда с текстом;
- минимум таблиц;
- карточки для обращений;
- понятные статусы;
- быстрые действия;
- подтверждение опасных действий;
- пустые состояния;
- хлебные крошки;
- подсказки;
- все тексты на русском;
- без технических терминов.

### 22.3 Главная админки

Показывать:

- новые обращения;
- обращения за неделю;
- последние обращения;
- клики в мессенджеры;
- завершённые расчёты;
- быстрые действия:
  - «Добавить работу»;
  - «Изменить цены»;
  - «Добавить отзыв»;
  - «Открыть сайт»;
  - «Посмотреть аналитику».

### 22.4 Onboarding / Quiz

При первом входе показать обучение:

1. Приветствие.
2. Где смотреть новые обращения.
3. Как изменить цену ткани.
4. Как добавить фото в галерею.
5. Как посмотреть аналитику.
6. Как обновить контакты.
7. Завершение.

Флаг хранить в `AdminUser.HasCompletedOnboarding`.

---

## 23. Аналитика и метрики

### 23.1 Внутренняя аналитика

Внутренняя аналитика обязательна и должна работать без платных сервисов.

События:

```text
page_view
hero_cta_clicked
gallery_item_opened
gallery_similar_clicked
calculator_started
calculator_completed
whatsapp_clicked
telegram_clicked
vk_clicked
inquiry_submitted
faq_opened
admin_login
admin_inquiry_status_changed
```

Правила:

- не отправлять персональные данные в аналитику;
- user agent хранить только в виде hash/обобщённых данных, если нужен;
- IP не хранить полностью;
- иметь возможность отключить аналитику через env;
- события должны быть лёгкими и не ломать UX, если аналитика временно недоступна.

### 23.2 Admin analytics dashboard

Страница:

```text
/admin/analytics
```

Показывать:

- посещения сайта по дням;
- обращения по дням;
- клики в WhatsApp / Telegram / VK;
- завершённые расчёты;
- популярные размеры;
- популярные ткани;
- популярные формы листьев;
- самые открываемые работы;
- воронку:
  - просмотр сайта;
  - запуск калькулятора;
  - завершение расчёта;
  - клик в мессенджер;
  - отправка обращения;
- статусы обращений;
- конверсию обращений в завершённые.

Визуализация:

- line chart;
- bar chart;
- pie/donut chart;
- карточки показателей.

Использовать бесплатную библиотеку:

- Chart.js;
- vue-chartjs.

### 23.3 Umami

Umami можно подключить как optional profile:

```bash
docker compose --profile analytics up -d
```

Umami нужен для:

- page views;
- источников трафика;
- браузеров;
- устройств;
- географии, если доступно.

Но админская аналитика не должна полностью зависеть от Umami.

---

## 24. Безопасность

Главный принцип:

```text
Вся бизнес-логика, которую можно подделать на frontend, должна выполняться на backend.
```

### 24.1 Запрещено делать на frontend

- финально считать цену;
- доверять рассчитанной цене от клиента;
- менять статус обращения без backend-авторизации;
- определять права пользователя;
- хранить роль пользователя;
- хранить access token в localStorage;
- хранить секреты;
- решать, какие admin данные можно показывать без backend проверки.

### 24.2 Auth

JWT только для админки.

- access token: короткоживущий;
- refresh token: httpOnly Secure SameSite cookie;
- refresh token хранить в БД только в виде hash;
- logout инвалидирует refresh token;
- все `/admin` backend endpoints требуют роль Admin.

### 24.3 Passwords

- BCrypt или Argon2;
- пароли никогда не хранить в открытом виде;
- dev admin создаётся из env.

### 24.4 Rate limiting

Добавить лимиты:

```text
POST public/inquiries: 5 запросов / 10 минут / IP
POST public/calculator/quote: 30 запросов / минуту / IP
POST admin/auth/login: 5 попыток / 15 минут / IP
POST public/analytics/events: ограничить батчами и частотой
```

### 24.5 Input validation

- FluentValidation на каждый Command/Query;
- ограничения длины строк;
- проверка enum значений;
- проверка активности выбранных справочников;
- санитизация текста;
- запрет raw HTML от пользователей.

### 24.6 HTTP headers

Добавить:

- Content-Security-Policy;
- X-Content-Type-Options: nosniff;
- X-Frame-Options: DENY;
- Referrer-Policy;
- Permissions-Policy.

### 24.7 Files

Если реализуется загрузка:

- проверять MIME;
- проверять magic bytes;
- максимальный размер;
- только JPEG/PNG/WebP;
- имена файлов UUID;
- не хранить оригинальное имя как путь;
- не отдавать исполняемые файлы.

### 24.8 Personal data

Формы собирают персональные данные, поэтому обязательно:

- чекбокс согласия;
- страница `/privacy`;
- минимизация данных;
- не логировать персональные данные без необходимости;
- не отправлять персональные данные в аналитику;
- не показывать персональные данные в ошибках.

---

## 25. Performance и VPS resource efficiency

Требования:

- API p95 < 300ms при малой нагрузке;
- изображения lazy loading;
- WebP;
- pagination;
- индексы БД;
- кэш только где полезно;
- не overengineering;
- не создавать тяжёлый background worker без необходимости;
- Nuxt SSR только для публичных SEO-страниц;
- админка может быть client-side;
- логи с ротацией.

PostgreSQL production настройки для малого VPS:

```text
shared_buffers=128MB
max_connections=20
effective_cache_size=256MB
```

Valkey:

```text
maxmemory 64mb
maxmemory-policy allkeys-lru
```

Контейнеры должны иметь примерные memory limits в production compose.

---

## 26. Docker Compose

Использовать profiles:

- `core` — nginx, api, frontend, postgres;
- `cache` — Valkey;
- `media` — MinIO;
- `analytics` — Umami.

Для простого запуска dev можно поднимать всё.

Production может поднимать минимальный набор:

```bash
docker compose --profile core --profile cache --profile media up -d
```

Umami отдельно:

```bash
docker compose --profile analytics up -d
```

---

## 27. .env.example

Создать `.env.example`:

```dotenv
# Environment
ASPNETCORE_ENVIRONMENT=Development
NODE_ENV=development

# Database
DB_HOST=db
DB_PORT=5432
DB_NAME=listopled
DB_USER=listopled_user
DB_PASSWORD=change_me

# JWT
JWT_SECRET=change_me_min_32_chars
JWT_ISSUER=listopled-api
JWT_AUDIENCE=listopled-client

# Admin seed
ADMIN_EMAIL=admin@listopled.local
ADMIN_PASSWORD=Admin123!
ADMIN_DISPLAY_NAME=Анна

# CORS
CORS_ORIGIN=http://localhost:3000

# Internal API
INTERNAL_API_BASE_URL=http://api:8080/api/v1

# Storage
STORAGE_PROVIDER=Local
MINIO_ENDPOINT=minio:9000
MINIO_ACCESS_KEY=minioadmin
MINIO_SECRET_KEY=change_me
MINIO_BUCKET=listopled-media
MINIO_USE_SSL=false

# Cache
CACHE_PROVIDER=Valkey
REDIS_CONNECTION=redis:6379

# Analytics
ANALYTICS_ENABLED=true
UMAMI_ENABLED=false
UMAMI_WEBSITE_ID=
UMAMI_SCRIPT_URL=

# SMTP
SMTP_ENABLED=false
SMTP_HOST=
SMTP_PORT=587
SMTP_USER=
SMTP_PASSWORD=
SMTP_FROM=
```

---

## 28. Документация в doc/

Codex обязан в первичной работе создать `doc/` в корне проекта и заполнить все `.md` файлы.

### 28.1 Обязательные файлы

```text
doc/README.md
doc/PRODUCT_VISION.md
doc/MARKET_AND_CRO_ANALYSIS.md
doc/TECHNICAL_SPECIFICATION.md
doc/ARCHITECTURE.md
doc/DOMAIN.md
doc/DATABASE_SCHEMA.md
doc/API.md
doc/UI_UX_GUIDE.md
doc/ADMIN_UX_GUIDE.md
doc/ANALYTICS.md
doc/SECURITY_AND_PRIVACY.md
doc/DEPLOYMENT.md
doc/DEV_SETUP.md
doc/DEVELOPMENT_RULES.md
doc/ASSUMPTIONS.md
doc/CHANGELOG.md
```

### 28.2 Правило документации

Сначала документация, потом код.

Если меняется:

- API — обновить `doc/API.md`;
- доменная модель — обновить `doc/DOMAIN.md` и `doc/DATABASE_SCHEMA.md`;
- архитектура — обновить `doc/ARCHITECTURE.md`;
- безопасность — обновить `doc/SECURITY_AND_PRIVACY.md`;
- аналитика — обновить `doc/ANALYTICS.md`;
- деплой — обновить `doc/DEPLOYMENT.md`;
- значимое решение — обновить `doc/ASSUMPTIONS.md` и `doc/CHANGELOG.md`.

---

## 29. Тесты

Обязательные unit tests:

- расчёт цены;
- расчёт цены без скидки;
- расчёт цены с неактивной тканью;
- расчёт цены с неактивным размером;
- SubmitInquiry validation;
- UpdateInquiryStatus;
- генерация InquiryNumber;
- analytics event без персональных данных;
- AutoMapper configuration validity.

Integration tests:

- calculator quote endpoint;
- submit inquiry endpoint;
- admin auth;
- admin protected endpoint returns 401 without token;
- swagger/scalar availability in Development if testable.

---

## 30. Seed data

Seed должен создать:

- admin user из env;
- размеры;
- ткани;
- формы листьев;
- скидки;
- base amount 4000;
- FAQ;
- контакты-заглушки;
- site settings;
- testimonials-заглушки;
- gallery items на основе `static/photos`, если возможно.

Dev admin:

```text
email: admin@listopled.local
password: Admin123!
```

Этот пароль только для dev.

---

## 31. Соглашения разработки

### 31.1 C#

- PascalCase для типов;
- PascalCase для public members;
- `_camelCase` для private fields;
- async methods end with `Async`;
- nullable reference types enabled;
- no business logic in controllers;
- CQRS handlers не должны разрастаться без нужды;
- AutoMapper profile должен валидироваться тестом.

### 31.2 TypeScript / Vue

- PascalCase для компонентов;
- camelCase для функций и переменных;
- типы в `types/`;
- без `any` без причины;
- общие UI-компоненты в `components/ui`;
- бизнес-фичи в отдельных папках.

### 31.3 Git

Conventional commits:

```text
feat(calculator): add price quote endpoint
fix(auth): invalidate refresh token on logout
docs(api): update inquiry endpoint contract
chore(docker): add production memory limits
```

---

## 32. Фазы реализации

### Фаза 1 — Документация и скелет

- создать структуру проекта;
- создать `doc/`;
- заполнить все md-файлы;
- создать backend solution;
- создать Nuxt app;
- создать docker compose;
- создать nginx configs;
- создать `.env.example`;
- создать README.

### Фаза 2 — Calculator vertical slice

- domain entities для калькулятора;
- EF configs;
- migrations;
- seed data;
- CalculatePriceQuery;
- GetCalculatorOptionsQuery;
- backend endpoints;
- Nuxt BFF routes;
- frontend calculator component;
- WhatsApp message helper;
- unit tests.

### Фаза 3 — Inquiries

- Inquiry entity;
- SubmitInquiryCommand;
- validation;
- public form;
- admin list;
- update status;
- email notification optional;
- tests.

### Фаза 4 — Admin MVP

- admin auth;
- dashboard;
- leads/inquiries;
- calculator management;
- gallery management;
- FAQ;
- testimonials;
- contacts;
- onboarding.

### Фаза 5 — Analytics

- AnalyticsEvent;
- trackEvent helper;
- admin analytics endpoint;
- charts;
- optional Umami profile.

### Фаза 6 — Polish

- SEO;
- sitemap;
- robots;
- accessibility;
- performance;
- responsive QA;
- production deployment docs;
- final docs sync.

---

## 33. Критерии готовности MVP

MVP готов, если:

- проект запускается через Docker Compose;
- dev и production конфигурации разделены;
- backend на .NET 10;
- frontend на Nuxt 3;
- API docs доступны в dev;
- API docs скрыты в production;
- backend API скрыт от прямого браузерного доступа в production;
- публичный сайт открывается;
- калькулятор работает;
- цена считается на backend;
- пример 7500 ₽ покрыт тестом;
- заявка сохраняется;
- WhatsApp message формируется;
- админ может войти;
- админ видит обращения;
- админ меняет статусы;
- админ управляет калькулятором;
- админ видит метрики;
- есть графики аналитики;
- есть галерея;
- есть отзывы;
- есть FAQ;
- есть контакты;
- интерфейс на русском;
- есть `doc/` со всей документацией;
- нет платных обязательных сервисов;
- проект можно запустить на минимальном VPS.

---

## 34. Поведение Codex

При каждом новом сеансе:

1. Прочитай `MASTER_PROMPT.md`.
2. Прочитай `doc/`.
3. Определи текущую фазу.
4. Проверь, не противоречит ли задача ТЗ.
5. Если противоречит — сначала предложи изменение документации.
6. Не реализуй оплату в MVP.
7. Не реализуй личный кабинет клиента в MVP.
8. Не превращай проект в маркетплейс.
9. Не добавляй платные технологии.
10. Не раскрывай backend API напрямую наружу в production.
11. Не переноси бизнес-логику на frontend.
12. После изменения API обновляй `doc/API.md`.
13. После изменения бизнес-логики обновляй `doc/DOMAIN.md`.
14. После изменения инфраструктуры обновляй `doc/DEPLOYMENT.md`.
15. После значимых изменений обновляй `doc/CHANGELOG.md`.

---

## 35. Главное правило

Проект создаётся не ради технологий.

Он должен помогать маленькому handmade-бренду продавать авторские пледы через:

- красоту;
- доверие;
- понятный расчёт цены;
- простую связь с мастером;
- удобную админку;
- аналитику для улучшения продукта.

Всё, что мешает сценарию:

```text
посмотрел → рассчитал → написал мастеру
```

не должно попадать в MVP.
