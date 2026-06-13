# Technical Specification

## Стек

Backend:

- .NET 10.
- ASP.NET Core 10 Web API.
- PostgreSQL 16.
- Entity Framework Core.
- CQRS + MediatR.
- FluentValidation.
- AutoMapper.
- JWT для админки.
- Serilog.
- OpenAPI.
- xUnit.
- Docker.

Frontend:

- Nuxt 3.
- Vue 3.
- TypeScript.
- Vite.
- Pinia.
- Tailwind CSS.
- CSS variables.
- VeeValidate + Zod.
- Chart.js / vue-chartjs.
- Nuxt server routes как BFF.

Infrastructure:

- Nginx reverse proxy.
- PostgreSQL.
- Valkey через `ICacheService`.
- MinIO или local storage через `IFileStorageService`.
- Optional Umami через Docker profile `analytics`.

## Функциональность MVP

- Публичные страницы: главная, галерея, калькулятор, о мастере, отзывы, privacy.
- Калькулятор с расчетом цены на backend.
- Форма обращения с согласием на обработку персональных данных.
- Генерация текста для WhatsApp.
- Админка: обращения, галерея, калькулятор, отзывы, FAQ, контакты, аналитика, настройки, onboarding.
- Внутренние события аналитики.
- OpenAPI в Development.

## Нефункциональные требования

- Интерфейс на русском языке.
- Mobile-first публичная часть.
- Админка удобна на ноутбуке и планшете.
- API p95 < 300ms при малой нагрузке.
- Пагинация для списков.
- Lazy loading и WebP для изображений.
- Без платных обязательных сервисов.
- Production рассчитан на VPS 1-2 vCPU, 1-2 GB RAM, 20 GB SSD.

## Тесты

Обязательные unit tests:

- расчет цены;
- расчет цены без скидки;
- расчет цены с неактивной тканью;
- расчет цены с неактивным размером;
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
- OpenAPI availability in Development if testable.

## Готовность MVP

MVP готов, когда проект запускается через Docker Compose, backend на .NET 10, frontend на Nuxt 3, калькулятор считает цену на backend, пример 7500 рублей покрыт тестом, обращение сохраняется, админка работает, аналитика видна, API docs доступны только в dev, backend API скрыт от прямого доступа в production, а документация актуальна.
