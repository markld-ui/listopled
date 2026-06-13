# LISTOPLED

LISTOPLED is a fullstack project for a small handmade brand that sells custom leaf-shaped blankets.

Current phase: Phase 1 — documentation and safe project skeleton.

## Scope Of Phase 1

Phase 1 creates only the project structure, documentation, backend/frontend skeletons, Docker/nginx placeholders, and security-oriented ignore files.

Phase 1 intentionally does not implement:

- calculator business logic;
- authentication;
- admin panel features;
- analytics;
- inquiry processing;
- public product UI.

## Source Of Truth

Before changing the project, read:

1. `MASTER_PROMPT_LISTOPLED.md`
2. `AGENTS.md`
3. `doc/`

## Local Checks

Backend skeleton:

```bash
dotnet restore backend/Listopled.sln
dotnet build backend/Listopled.sln
```

Frontend skeleton:

```bash
cd frontend
npm install
npm run build
```

Do not create a real `.env`. Use `.env.example` as a placeholder template.
