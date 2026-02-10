# Strangler Fig — Proof of Concept

**Migrate a legacy system piece by piece, behind one URL, with no big-bang rewrite.**

This repo is a small, runnable demo of the **Strangler Fig** pattern: one reverse proxy, one legacy app, one modern app. Users see a single site; under the hood, each request is routed to the right backend. You get incremental migration, rollback by config, and zero URL churn.

---

## What is the Strangler Fig pattern?

The name comes from strangler fig trees: they grow around an existing tree and eventually replace it, while the host tree keeps functioning until it’s no longer needed.

In software:

- You **don’t** replace the monolith in one go.
- You put a **facade** (proxy) in front of it. All traffic goes through the facade.
- You build **new behaviour** in a separate system (microservice, new app, etc.).
- The facade **routes by URL** (or other rules): “this path → new system, everything else → legacy.”
- Over time you move more routes to the new system. When nothing hits the old one, you retire it.

**Benefits:** Lower risk, continuous delivery, clear rollback (flip a route back to legacy), and users never see “we’re under construction” or different domains.

---

## What this solution demonstrates

| You see | What’s really happening |
|--------|--------------------------|
| One site at `https://localhost:5000` | A **reverse proxy** (ProxyFacade) receiving every request |
| Modern home at `/` | Proxy forwards to **ModernMvc** (new app) |
| Legacy contact at `/contact` | Proxy forwards to **LegacyMvc** (original monolith) |
| Same URL bar everywhere | No redirects; proxy forwards the request and streams the response back |

So: **one domain, two backends, incremental migration.**

---

## How it fits together

```
                    ┌─────────────────────────────────────────┐
                    │  User only ever uses                     │
                    │  https://localhost:5000                  │
                    └──────────────────┬──────────────────────┘
                                       │
                                       ▼
                    ┌──────────────────────────────────────────┐
                    │  ProxyFacade (reverse proxy)              │
                    │  • /         → ModernMvc (modernised)     │
                    │  • /contact  → LegacyMvc (unchanged)     │
                    │  • else      → LegacyMvc (default)        │
                    └──────────────┬───────────────┬────────────┘
                                   │               │
                    ┌──────────────▼───┐   ┌───────▼────────────┐
                    │  ModernMvc       │   │  LegacyMvc         │
                    │  :5002          │   │  :5001             │
                    │  New home UI    │   │  Old home + Contact │
                    └─────────────────┘   └────────────────────┘
```

- **ProxyFacade** — The only app the user hits. Uses [YARP](https://microsoft.github.io/reverse-proxy/) to forward requests; supports a **rollback toggle** so `/` can be switched back to legacy for demos.
- **LegacyMvc** — The existing monolith. Serves legacy Home and Contact; no direct user traffic, only from the proxy.
- **ModernMvc** — The modernised slice. Serves the new Home dashboard; more routes can be added here as you “strangle” the monolith.

---

## The three projects

| Project | Role | Port | README |
|---------|------|------|--------|
| **ProxyFacade** | Single entry point; reverse proxy; route-by-URL | 5000 | [ProxyFacade/README.md](ProxyFacade/README.md) |
| **LegacyMvc** | Legacy monolith (home + contact) | 5001 | [LegacyMvc/README.md](LegacyMvc/README.md) |
| **ModernMvc** | Modernised slice (new home) | 5002 | [ModernMvc/README.md](ModernMvc/README.md) |

**Stack:** ASP.NET Core MVC, .NET 9, C#. HTTPS only.

---

## Run it

1. **Start all three apps** (each in its own terminal, or use your IDE’s “multiple startup projects”):

   ```bash
   # Terminal 1 — the facade (user-facing)
   dotnet run --project ProxyFacade

   # Terminal 2 — legacy backend
   dotnet run --project LegacyMvc

   # Terminal 3 — modern backend
   dotnet run --project ModernMvc
   ```

2. **Use only the proxy URL:**  
   Open **https://localhost:5000** in your browser.

3. **Try:**
   - **https://localhost:5000/** → modernised home (or legacy home if rollback is on).
   - **https://localhost:5000/contact** → legacy contact.

You never need to open 5001 or 5002; the proxy hides the two backends.

---

## What you’ll take away

- A **concrete example** of Strangler Fig: proxy + legacy + modern, routing by URL.
- **One domain**, two systems, no redirects, no URL changes for users.
- **Rollback** via config (e.g. “send `/` to legacy again”) instead of redeploys.
- A structure you can extend: add more routes to ModernMvc and adjust proxy config as you migrate.

For per-app details, routes, and verification steps, see each project’s README linked in the table above.
