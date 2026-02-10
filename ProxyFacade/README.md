# ProxyFacade — Strangler Fig Reverse Proxy

## What ProxyFacade Does

ProxyFacade is the **single public entry point** for the application. It is the only app users hit in the browser (e.g. `https://localhost:5000`). It acts as a **reverse proxy**: it receives each request, decides whether that URL has been “modernised” or not, and **forwards the request** to either the legacy backend (LegacyMvc) or the modern backend (ModernMvc). The browser URL never changes and the user never sees redirects or different ports—so you get one domain, two backends, and incremental migration (strangler fig pattern) without downtime.

---

## What ProxyFacade Is Responsible For

- **Single entry point** — Users always use `https://localhost:5000`. They never type 5001 or 5002.
- **Reverse proxy** — Forwards requests to LegacyMvc or ModernMvc based on URL rules. No HTTP redirects (redirects would change the URL and break the “same domain” illusion).
- **Route decision logic** — Decides “changed” (modern) vs “unchanged” (legacy) and supports a **rollback toggle** for demos.

---

## Routing Rules (Strangler Fig Simulation)

| URL | Backend | Notes |
|-----|---------|--------|
| `/` (Home) | **ModernMvc** | Modernised home (or Legacy if rollback is on) |
| `/contact` | **LegacyMvc** | Legacy-only; do not create Contact in ModernMvc |
| Anything else | **LegacyMvc** | Safe default (e.g. `/privacy`, `/foo`, etc.) |

**Route order matters:** define specific routes first, fallback last.

---

## Verification

1. Run all three apps (ProxyFacade, LegacyMvc, ModernMvc).
2. Use **only** the proxy URL: `https://localhost:5000`.

| Test | Expected |
|------|----------|
| `https://localhost:5000/` | Modern Home (or Legacy Home if rollback is on) |
| `https://localhost:5000/contact` | Legacy Contact |

**Ports (for reference):**

- **ProxyFacade** → `https://localhost:5000` (public entry)
- **LegacyMvc** → `https://localhost:5001` (backend)
- **ModernMvc** → `https://localhost:5002` (backend)

---

## What the Demo Should Feel Like

- One domain the whole time: `https://localhost:5000`.
- `/` → modern (or legacy with rollback).
- `/contact` → legacy.
- Same domain, two systems, incremental migration, no downtime.
