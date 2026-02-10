# Legacy MVC

## What LegacyMvc Does

LegacyMvc is the **existing monolith** backend in the Strangler Fig PoC. It is not the app users hit directly—ProxyFacade forwards requests here for URLs that have not yet been modernised. It serves the legacy Home and Contact pages and is gradually replaced as routes are moved to ModernMvc. See [solution README](../README.md) for ports and running the whole solution.

---

## Purpose

- **Home (`/`):** Legacy dashboard; badge **LEGACY — WILL BE MODERNISED**. Old-style UI (serif, muted).
- **Contact (`/contact`):** Unchanged legacy page; badge **LEGACY — NOT MODERNISED**.

## Routes

| Route      | Description   | Action            |
|-----------|----------------|-------------------|
| `/`       | Legacy Home    | `HomeController.Index`  |
| `/contact`| Legacy Contact | `HomeController.Contact` |

## Run

From repo root: `dotnet run --project LegacyMvc`  
Or from this folder: `dotnet run`  
→ https://localhost:5001

## Structure (relevant)

- `Controllers/HomeController.cs` — Index, Contact
- `Views/Home/Index.cshtml`, `Contact.cshtml`
- `wwwroot/css/legacy-dashboard.css`, `contact-page.css`, `site.css`
