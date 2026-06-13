# Deployment

## Среды

Проект поддерживает две среды:

- `Development`;
- `Production`.

## Development

В dev:

- API docs доступны;
- frontend hot reload;
- backend logs Debug/Information;
- PostgreSQL можно открыть наружу на `localhost:5432`;
- Valkey можно открыть наружу на `localhost:6379`;
- MinIO console доступна, если profile включен;
- CORS разрешает localhost;
- seed создает dev admin;
- backend и frontend можно запускать отдельно.

## Production

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
- `/admin` защищен авторизацией;
- неизвестные API routes возвращают 404.

## Docker Compose profiles

- `core` — nginx, api, frontend, postgres.
- `cache` — Valkey.
- `media` — MinIO.
- `analytics` — Umami.

Production минимальный пример:

```bash
docker compose --profile core --profile cache --profile media up -d
```

Umami отдельно:

```bash
docker compose --profile analytics up -d
```

## Ресурсы VPS

Целевая конфигурация:

```text
CPU: 1-2 vCPU
RAM: 1-2 GB
Disk: 20 GB SSD
```

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

## Storage decision

Проект обязан иметь `IFileStorageService`. Фактический production-вариант — Local или MinIO — нужно утвердить перед настройкой production compose.
