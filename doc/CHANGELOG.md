# Changelog

## 2026-06-14

- Создан первичный комплект документации в `doc/`.
- Зафиксирован технический план Фазы 1.
- Зафиксированы риски, противоречия и допущения.
- Подтверждено, что текущая итерация не создает backend/frontend код.
- Уточнено ограничение Фазы 1: готовятся только документация и безопасный каркас, без реализации калькулятора, авторизации, админки, аналитики, обращений или публичного продукта.
- Добавлен безопасный каркас проекта: root ignore/config files, Docker/nginx skeleton, production compose skeleton, backend .NET solution skeleton, frontend Nuxt skeleton и `doc/ADMIN_GUIDE.md`.
- Усилены ignore-правила для `static/photos`: приватные брендовые материалы исключены из git и Docker build context, оставлены только `.gitkeep` и `README.md`.
- Уточнена рамка Фазы 2: `Public Landing + Calculator Conversion Slice`, где калькулятор является частью публичного конверсионного пути лендинга, а не изолированной технической фичей.
- Добавлена декомпозиция Фазы 2 на маленькие будущие шаги от backend infrastructure packages до landing conversion polish и финальной синхронизации документации.
- Добавлен backend infrastructure foundation для шага 2.1-B: Central Package Management, `global.json`, утвержденные NuGet package references, basic Application/Infrastructure DI, Swashbuckle только для Development и минимальные smoke tests.
- Зафиксировано, что Serilog, EF Design, Testcontainers/WebApplicationFactory и mocking framework отложены; калькулятор, migrations, seed data и public API endpoints не реализованы.
- Добавлен `xunit.runner.visualstudio` как минимальный adapter для обнаружения xUnit v3 tests через `dotnet test`.
- Добавлены доменные сущности калькулятора для шага 2.2: `BlanketSize`, `Fabric`, `LeafShape`, `Discount`, `PriceCalculationSettings`, `PriceQuote`, `DomainException` и unit tests доменных инвариантов.
- Зафиксировано, что EF configurations, migrations, seed data, CQRS, API endpoints и frontend на шаге 2.2 не реализуются.
- Добавлен чистый доменный `PriceCalculatorService` для шага 2.3 с расчетом `F + T * R + B - I`, проверкой активных опций, обнулением отрицательного итога и unit tests, включая обязательный пример `7500 ₽`.
- Зафиксировано, что EF configurations, migrations, seed data, CQRS, API endpoints и frontend на шаге 2.3 не реализуются.
- Утверждена persistence strategy калькулятора: справочники и настройки хранятся в БД, `PriceQuotes` table не создается в MVP, расчет сохраняется только как snapshot внутри будущего `Inquiry`.
- Добавлен шаг 2.4-B1: `AppDbContext`, `DbSet` и EF configurations для calculator catalogs/settings в `Listopled.Infrastructure`; `Listopled.Application` остается без EF Core, `PriceQuote` не маппится, migrations и seed data не созданы.
