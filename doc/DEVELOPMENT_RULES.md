# Development Rules

## Общие правила

- Сначала документация, потом код.
- Перед новой задачей читать `MASTER_PROMPT_LISTOPLED.md` и `doc/`.
- Не менять стек без утверждения.
- Не добавлять платные обязательные сервисы.
- Не превращать MVP в маркетплейс.
- Не реализовывать оплату, корзину и личный кабинет клиента в MVP.
- Backend остается source of truth для бизнес-логики.
- Фаза 2 реализуется как `Public Landing + Calculator Conversion Slice`, а не как изолированный калькулятор.
- Перед каждым шагом Фазы 2 нужен отдельный план с файлами, зависимостями, миграциями, тестами и рисками.

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

## Когда обновлять документацию

- API изменился — обновить `doc/API.md`.
- Доменная модель изменилась — обновить `doc/DOMAIN.md` и `doc/DATABASE_SCHEMA.md`.
- Архитектура изменилась — обновить `doc/ARCHITECTURE.md`.
- Безопасность изменилась — обновить `doc/SECURITY_AND_PRIVACY.md`.
- Аналитика изменилась — обновить `doc/ANALYTICS.md`.
- Деплой изменился — обновить `doc/DEPLOYMENT.md`.
- Значимое решение принято — обновить `doc/ASSUMPTIONS.md` и `doc/CHANGELOG.md`.

## C#

- PascalCase для типов.
- PascalCase для public members.
- `_camelCase` для private fields.
- Async methods end with `Async`.
- Nullable reference types enabled.
- No business logic in controllers.
- CQRS handlers не должны разрастаться без нужды.
- AutoMapper profile должен валидироваться тестом.

## TypeScript / Vue

- PascalCase для компонентов.
- camelCase для функций и переменных.
- Типы в `types/`.
- Без `any` без причины.
- Общие UI-компоненты в `components/ui`.
- Бизнес-фичи в отдельных папках.

## Git

Использовать Conventional Commits:

```text
feat(calculator): add price quote endpoint
fix(auth): invalidate refresh token on logout
docs(api): update inquiry endpoint contract
chore(docker): add production memory limits
```

## Critical decisions

Для критичных решений нужно сначала оформить предложение изменения и дождаться утверждения пользователя.
