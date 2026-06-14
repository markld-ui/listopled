# Analytics

## Принцип

Внутренняя аналитика обязательна и должна работать без платных сервисов. Umami допускается только как optional self-hosted profile и не должен быть жесткой зависимостью сайта.

## События

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

## Правила сбора

- Не отправлять персональные данные в аналитику.
- IP не хранить полностью.
- User agent хранить только в виде hash или обобщенных данных, если нужен.
- Аналитику можно отключить через env.
- События не должны ломать UX, если аналитика временно недоступна.
- `POST public/analytics/events` ограничивать батчами и частотой.
- Незавершенные расчеты учитываются через `AnalyticsEvent`, например `calculator_started` и `calculator_completed`, без персональных данных.

## Admin dashboard

Страница:

```text
/admin/analytics
```

Показывать:

- посещения сайта по дням;
- обращения по дням;
- клики в WhatsApp / Telegram / VK;
- завершенные расчеты;
- популярные размеры;
- популярные ткани;
- популярные формы листьев;
- самые открываемые работы;
- воронку: просмотр сайта → запуск калькулятора → завершение расчета → клик в мессенджер → отправка обращения;
- статусы обращений;
- конверсию обращений в завершенные.

## Визуализация

Использовать бесплатные библиотеки:

- Chart.js;
- vue-chartjs.

Типы графиков:

- line chart;
- bar chart;
- pie/donut chart;
- карточки показателей.

## Umami

Optional profile:

```bash
docker compose --profile analytics up -d
```

Umami нужен для page views, источников трафика, браузеров, устройств и географии, если доступно. Админская аналитика не должна полностью зависеть от Umami.
