# API

## Base URLs

Internal backend:

```text
http://api:8080/api/v1
```

Browser-facing Nuxt BFF:

```text
/api/public/...
/api/admin/...
```

В production ASP.NET API не должен быть напрямую доступен извне.

## Response format

Success:

```json
{
  "success": true,
  "data": {},
  "error": null,
  "meta": null
}
```

Error:

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

## Public backend endpoints

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

## Phase 2 public conversion API scope

Фаза 2 использует существующие утвержденные public endpoints как единый conversion slice лендинга:

- `GET /api/v1/public/site-settings` — базовые тексты и настройки публичной страницы.
- `GET /api/v1/public/contact-settings` — контакты и ссылки для CTA в мессенджеры.
- `GET /api/v1/public/gallery` — работы/галерея для доверия и выбора референса.
- `GET /api/v1/public/testimonials` — отзывы и социальное доказательство.
- `GET /api/v1/public/faq` — ответы на вопросы перед обращением.
- `GET /api/v1/public/calculator/options` — справочники калькулятора.
- `POST /api/v1/public/calculator/quote` — предварительный расчет цены на backend.

Калькулятор не должен становиться отдельным публичным продуктом вне лендинга. Его API используется в связке: контент → работы → расчет → CTA → обращение к мастеру.

`POST /api/v1/public/inquiries` остается утвержденной точкой для будущей связки с формой обращения, но полноценная реализация обращений относится к отдельному следующему срезу.

## Admin backend endpoints

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

## Nuxt BFF route examples

```text
/api/public/calculator/options
/api/public/calculator/quote
/api/public/inquiries
/api/admin/inquiries
/api/admin/auth/login
```

BFF route names may be more frontend-friendly, but they must map to approved backend endpoints and must not add new business behavior.

Open point: the master prompt includes one Nuxt BFF example with `leads`, while the backend API and product language use `inquiries` / «обращения». Do not implement BFF route names until this naming is confirmed.

## Forbidden in MVP

```text
/api/v1/auth/register
/api/v1/users/me
/api/v1/cart
/api/v1/wishlist
/api/v1/payments
/api/v1/customer/orders
```

## OpenAPI

OpenAPI documentation is available only in `Development`. In `Production`, Swagger / Scalar / OpenAPI UI must return `404`.
