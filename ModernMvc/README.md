# Modern MVC

## What ModernMvc Does

ModernMvc is the **modernised backend** in the Strangler Fig PoC. It is not the app users hit directly—ProxyFacade forwards requests here for URLs that have been migrated. It currently serves the new Home dashboard; more routes are added here as they are modernised. See [solution README](../README.md) for ports and running the whole solution.

---

## Purpose

- **Home (`/`):** Modern dashboard with badge **MODERNISED HOME**: quick actions (create / track / cancel order), order summary (total, pending, processing, shipped, delayed), recent orders.

## Routes

| Route | Description   | Action            |
|-------|----------------|-------------------|
| `/`   | Modernised Home | `HomeController.Index` |

## Run

From repo root: `dotnet run --project ModernMvc`  
Or from this folder: `dotnet run`  
→ https://localhost:5002

## Structure (relevant)

- `Controllers/HomeController.cs` — Index (dashboard with sample summary counts)
- `Views/Home/Index.cshtml` — Hero, actions, quick-start tiles, order summary, recent orders
- `wwwroot/css/modern-dashboard.css` — Same design system as Legacy Contact
