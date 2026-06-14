# LISTOPLED Documentation

Документация является рабочим слоем между `MASTER_PROMPT_LISTOPLED.md` и кодом. Если решение меняет API, доменную модель, архитектуру, безопасность, деплой или значимые правила проекта, сначала обновляется соответствующий файл в `doc/`, затем код.

## Текущая фаза

Фаза 2 — Public Landing + Calculator Conversion Slice.

Фаза 1 завершила безопасный каркас и документацию. Фаза 2 не должна восприниматься как изолированная техническая фича калькулятора: это первый публичный конверсионный срез лендинга.

Цель Фазы 2:

```text
Пользователь открывает сайт → видит красивый лендинг → понимает продукт → смотрит работы → считает примерную цену → получает CTA → может написать мастеру.
```

Главный сценарий MVP остается:

```text
Клиент увидел работы → понял ценность → рассчитал примерную цену → написал мастеру.
```

Калькулятор является важным блоком этого пути, но рядом с ним должны появиться структура публичной страницы, контентные блоки, галерея/работы, CTA в мессенджеры, блоки доверия и подготовка связки с формой обращения.

## План Фазы 1

1. Создать и заполнить `doc/`.
2. Зафиксировать продуктовую рамку MVP и запреты Post-MVP.
3. Описать архитектуру backend, frontend, BFF и reverse proxy.
4. Описать доменную модель MVP и первичную схему БД.
5. Описать API routes и запреты на routes вне MVP.
6. Описать UX публичной части и админки.
7. Описать аналитику, безопасность, приватность, деплой и dev setup.
8. Создать корневые правила для Codex в `AGENTS.md`.
9. Создать changelog.
10. После утверждения документации перейти к скелету проекта: backend solution, Nuxt app, Docker Compose, nginx, `.env.example`, README.

Важно: скелет проекта в Фазе 1 не должен включать рабочую реализацию калькулятора, auth, admin, analytics или inquiries. Максимум — безопасная структура, базовые конфиги и placeholders без бизнес-логики.

## Декомпозиция Фазы 2

1. Backend infrastructure packages and DI.
2. Calculator domain entities.
3. Calculator price calculation service + unit tests.
4. Calculator EF configurations, seed data and migrations.
5. Calculator CQRS + backend public API.
6. Public landing content backend API: site settings, contacts, gallery, testimonials, FAQ.
7. Nuxt BFF routes for public API.
8. Frontend design foundation: CSS variables, Tailwind config, базовые UI-компоненты.
9. Landing page skeleton.
10. Frontend calculator component.
11. WhatsApp CTA helper.
12. Landing conversion polish.
13. Final checks, docs sync and commit.

Фаза 2 должна идти маленькими задачами. Реализация каждого шага начинается только после отдельного утверждения плана, списка файлов, зависимостей, миграций и тестов.

## Потенциальные противоречия и риски

- В мастер-файле в разделе поведения упоминается `MASTER_PROMPT.md`, но фактический файл в репозитории и в задаче называется `MASTER_PROMPT_LISTOPLED.md`.
- Фаза 1 по мастер-файлу включает создание backend/Nuxt/docker/nginx, но текущая задача запрещает создавать backend/frontend код до готовности документации и плана.
- Production storage допускает MinIO или local storage через `IFileStorageService`; фактический вариант нужно выбрать перед деплоем.
- В разделе Nuxt BFF мастер-файл приводит пример routes с `leads`, а основной API и доменная терминология используют `inquiries` / «обращения». Перед реализацией BFF route names нужно утвердить единый термин.
- Полный список форм листьев предлагается взять с текущего сайта, но пока он не перенесен в репозиторий как структурированные данные.
- Финальные брендовые цвета, шрифты и макеты из Figma пока не предоставлены.
- Реальный текст политики обработки персональных данных требует отдельного юридического согласования перед production.

## Вопросы, которые не блокируют документацию, но нужны перед реализацией

- Какие реальные Telegram, WhatsApp, VK и email использовать в контактах?
- Использовать ли MinIO в production с первого релиза или начать с local storage?
- Утвердить ли `inquiries` как единый термин для backend API, BFF routes и UI, вместо встречающегося в примере `leads`?
- Нужно ли переносить полный список форм листьев с Tilda вручную перед seed data?
- Есть ли финальный домен, схема HTTPS и способ получения сертификатов?
- Нужна ли email-нотификация мастеру в MVP или оставить ее как optional SMTP?

## Обязательные файлы

- `PRODUCT_VISION.md` — продуктовая рамка.
- `MARKET_AND_CRO_ANALYSIS.md` — рынок, аудитория, CRO.
- `TECHNICAL_SPECIFICATION.md` — техническое ТЗ MVP.
- `ARCHITECTURE.md` — архитектура backend/frontend/infrastructure.
- `DOMAIN.md` — доменная модель и бизнес-правила.
- `DATABASE_SCHEMA.md` — первичная схема БД.
- `API.md` — API и BFF routes.
- `UI_UX_GUIDE.md` — публичный UX.
- `ADMIN_UX_GUIDE.md` — UX админки.
- `ADMIN_GUIDE.md` — будущая инструкция для мастера по работе с админкой.
- `ANALYTICS.md` — события и dashboard.
- `SECURITY_AND_PRIVACY.md` — безопасность и персональные данные.
- `DEPLOYMENT.md` — dev/prod деплой.
- `DEV_SETUP.md` — локальная разработка.
- `DEVELOPMENT_RULES.md` — правила разработки.
- `ASSUMPTIONS.md` — допущения и нерешенные вопросы.
- `CHANGELOG.md` — изменения документации и проекта.
