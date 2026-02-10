# LegacyMvc — Your Todo List

Use this checklist to run and verify the legacy system.

## Run & verify

- [ ] **Run Legacy MVC app**  
  From repo root: `dotnet run --project LegacyMvc`  
  Or from `LegacyMvc`: `dotnet run`  
  Ensure the **https** launch profile is used so the app listens on `https://localhost:5001`.

- [ ] **Open Home page**  
  Visit https://localhost:5001/  
  - Page shows “This is the current monolith.”  
  - Page has the intended “old” look (serif font, muted colors).

- [ ] **Open Contact page**  
  Visit https://localhost:5001/contact  
  - Simple contact content is shown.  
  - Nav has “Home” and “Contact” links.

- [ ] **Check navigation**  
  - From Home, click “Contact” and confirm you get `/contact`.  
  - From Contact, click “Home” (or “LegacyMvc”) and confirm you get `/`.

## Optional

- [ ] Trust the dev certificate if needed: `dotnet dev-certs https --trust`
- [ ] Run from Visual Studio or Rider using the **https** profile and confirm port 5001.

## Later (Strangler Fig PoC)

- [ ] Introduce a proxy/facade that can route some traffic to a new system while `/` and `/contact` still go to LegacyMvc.
- [ ] Migrate one route (e.g. a new “modern” home or contact) to the new system and route it via the proxy.
