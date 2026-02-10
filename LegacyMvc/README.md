# Legacy MVC — The Current Monolith

This project represents the **existing system** in the Strangler Fig proof-of-concept: a single ASP.NET Core MVC application that will eventually be gradually replaced by a modernized system.

## Overview

- **Language:** C#
- **Framework:** ASP.NET Core MVC
- **Runtime:** .NET 7
- **Port:** HTTPS `https://localhost:5001`, HTTP `http://localhost:5081`

**Solution port scheme (consistent across projects):**

| App         | HTTPS                  | HTTP                  |
|-------------|------------------------|------------------------|
| ProxyFacade | https://localhost:5000 | http://localhost:5080 |
| LegacyMvc   | https://localhost:5001 | http://localhost:5081 |
| ModernMvc   | https://localhost:5002 | http://localhost:5082 |

## Purpose

LegacyMvc is the “before” state: the current monolith. The home page explicitly states **“This is the current monolith.”** and uses a deliberately old-looking style. The contact page is kept simple and unchanged to represent legacy behavior.

## Routes

| Route     | Description      | Controller / Action   |
|----------|------------------|------------------------|
| `/`      | Legacy Home      | `HomeController.Index` |
| `/contact` | Legacy Contact | `HomeController.Contact` |

Additional routes (e.g. `/Home/Privacy`, `/Home/Error`) remain available for compatibility.

## Behavior

- **Home page (`/`):** Looks old (serif fonts, muted colors, simple layout) and displays the message *“This is the current monolith.”*
- **Contact page (`/contact`):** Simple, unchanged legacy page with basic contact info.

## How to Run

1. Open a terminal in the solution or project directory.
2. Run the app (HTTPS on port 5001):

   ```bash
   dotnet run --project LegacyMvc
   ```

   Or from the `LegacyMvc` folder:

   ```bash
   cd LegacyMvc
   dotnet run
   ```

3. In launch profiles, use the **https** profile so the app uses `https://localhost:5001`.
4. Open in a browser:
   - **Home:** https://localhost:5001/
   - **Contact:** https://localhost:5001/contact

## Project Structure (relevant parts)

```
LegacyMvc/
├── Controllers/
│   └── HomeController.cs    # Index, Contact, Privacy, Error
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml     # Legacy Home — “This is the current monolith.”
│   │   ├── Contact.cshtml  # Legacy Contact (simple, unchanged)
│   │   └── Privacy.cshtml
│   └── Shared/
│       └── _Layout.cshtml  # Nav: Home, Contact
├── wwwroot/
│   └── css/
│       └── site.css        # Legacy “old” styling for Home/Contact
├── Program.cs              # Route config: /, /contact
├── Properties/
│   └── launchSettings.json # https://localhost:5001
└── README.md               # This file
```

## Next Steps (Strangler Fig PoC)

In the broader PoC, this legacy app will be:

1. Put behind a proxy/facade that can route some URLs to a new system.
2. Gradually replaced by new features (e.g. new UI or APIs) while legacy routes continue to be served by this app until they are migrated.

For a concrete checklist of what to do with LegacyMvc (run, verify, etc.), see **TODO.md** in this folder or the project root.
