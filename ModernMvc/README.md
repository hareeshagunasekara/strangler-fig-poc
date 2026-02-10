# Modern MVC

The **modernised slice** in the Strangler Fig PoC — new home dashboard and UI. See [solution README](../README.md) for ports and running the whole solution.

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
