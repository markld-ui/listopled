# Dev Setup

Этот файл описывает целевую локальную разработку. Команды будут уточнены после создания backend solution, Nuxt app и Docker Compose.

## Предварительные требования

- .NET 10 SDK.
- Node.js LTS, совместимый с Nuxt 3.
- Docker Desktop или Docker Engine.
- Git.

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

## Документация API

В Development OpenAPI UI доступен. В Production недоступен и должен возвращать `404`.
