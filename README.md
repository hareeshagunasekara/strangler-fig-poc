# Strangler Fig PoC

Proof-of-concept for **Strangler Fig** migration: a single solution with a proxy and two apps (legacy monolith + modernised slice).

## Solution overview

| Project      | Role              | HTTPS                  |
|-------------|-------------------|------------------------|
| **ProxyFacade** | Entry point / router | https://localhost:5000 |
| **LegacyMvc**   | Current monolith      | https://localhost:5001 |
| **ModernMvc**   | Modernised slice      | https://localhost:5002 |

- **Stack:** ASP.NET Core MVC, .NET 7, C#
- **Ports:** HTTPS only; each app has its own port.

## How to run

**Run one app** (from repo root):

```bash
dotnet run --project ProxyFacade   # https://localhost:5000
dotnet run --project LegacyMvc     # https://localhost:5001
dotnet run --project ModernMvc     # https://localhost:5002
```

**Run the solution** in Visual Studio or Rider: set multiple startup projects (ProxyFacade, LegacyMvc, ModernMvc) or run each in a separate terminal.

## Projects

- **[LegacyMvc](LegacyMvc/README.md)** — Existing system: home (to be modernised) and contact (legacy-only). Red badges: “LEGACY — WILL BE MODERNISED” / “LEGACY — NOT MODERNISED”.
- **[ModernMvc](ModernMvc/README.md)** — New slice: modernised home with dashboard, quick actions, order summary. Badge: “MODERNISED HOME”.
- **ProxyFacade** — Routes traffic to Legacy or Modern (e.g. `/` → Modern, `/contact` → Legacy).

## Strangler Fig in this PoC

1. **Proxy** fronts both apps and routes by URL.
2. **Legacy** keeps serving unchanged routes (e.g. contact) and “will be modernised” routes (e.g. home) until replaced.
3. **Modern** implements new behaviour for migrated routes; users hitting the proxy see the new UI for those routes.

See project READMEs for routes and run instructions per app.
