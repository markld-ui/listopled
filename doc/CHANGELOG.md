# Changelog

## 2026-06-14

- Создан первичный комплект документации в `doc/`.
- Зафиксирован технический план Фазы 1.
- Зафиксированы риски, противоречия и допущения.
- Подтверждено, что текущая итерация не создает backend/frontend код.
- Уточнено ограничение Фазы 1: готовятся только документация и безопасный каркас, без реализации калькулятора, авторизации, админки, аналитики, обращений или публичного продукта.
- Добавлен безопасный каркас проекта: root ignore/config files, Docker/nginx skeleton, production compose skeleton, backend .NET solution skeleton, frontend Nuxt skeleton и `doc/ADMIN_GUIDE.md`.
- Усилены ignore-правила для `static/photos`: приватные брендовые материалы исключены из git и Docker build context, оставлены только `.gitkeep` и `README.md`.
