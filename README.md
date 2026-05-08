# DigiVault Web

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-9.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white)
![Razor Views](https://img.shields.io/badge/Razor_Views-MVC-512BD4?logo=dotnet&logoColor=white)
![License](https://img.shields.io/badge/license-MIT-green)
![Status](https://img.shields.io/badge/status-🚧_work_in_progress-yellow)
[![Live Intranet](https://img.shields.io/badge/Live-Intranet_Panel-512BD4?logo=dotnet&logoColor=white)](https://digivaultintranet.onrender.com)

Web front-end for **DigiVault** — a digital course marketplace. This solution contains two ASP.NET Core MVC applications: a customer-facing public portal and an internal administration panel (intranet). Both communicate with the [DigiVault API](https://github.com/AIChelminska/DigiVaultAPI) over HTTP.

> 🚧 This project is a work in progress. Some views and features are still being implemented.

### Live Deployments

| App | URL | Notes |
|-----|-----|-------|
| DigiVault.Intranet | [https://digivaultintranet.onrender.com](https://digivaultintranet.onrender.com) | Admin panel — full CRUD |
| DigiVaultAPI | [https://digivaultapi.onrender.com/swagger](https://digivaultapi.onrender.com/swagger) | REST API + Swagger UI |

> Both apps are hosted on Render's free tier. The first request after a period of inactivity may take up to 50 seconds (cold start).

---

## Table of Contents

- [Projects](#projects)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Configuration](#configuration)
  - [Running Locally](#running-locally)
- [Pages Overview](#pages-overview)
  - [PortalWWW](#portalwww)
  - [Intranet](#intranet)
- [Authentication](#authentication)
- [License](#license)

---

## Projects

| Project | Description |
|---------|-------------|
| `DigiVault.PortalWWW` | Public-facing customer portal — browse courses, manage cart and wishlist, view orders, log in |
| `DigiVault.Intranet` | Internal administration panel — full CRUD for users, courses, categories, orders, notifications, reports, and settings |

Both projects live in the same solution (`DigiVaultWeb.sln`) and target **.NET 9**.

---

## Tech Stack

| Technology | Version |
|---|---|
| [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/) | 9.0 |
| [Razor Views](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/) | — |
| [System.IdentityModel.Tokens.Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt) | 8.17.0 |
| Session-based auth (JWT stored server-side) | — |
| `HttpClient` + named client (`DigiVaultAPI`) | built-in |

---

## Project Structure

```
DigiVaultWeb/
├── DigiVault.PortalWWW/        # Customer portal
│   ├── Controllers/
│   │   ├── AccountController.cs    # Login / logout (JWT → session)
│   │   ├── HomeController.cs       # Home page — popular / newest / top-rated courses
│   │   ├── CoursesController.cs    # Course catalogue + course detail with reviews
│   │   ├── CartController.cs       # Shopping cart (stub view)
│   │   ├── WishlistController.cs   # Wishlist (stub view)
│   │   └── MyVaultController.cs    # Purchased courses (stub view)
│   ├── Models/                     # ViewModels and API DTOs
│   │   ├── CoursesIndexViewModel.cs
│   │   ├── CourseDetailViewModel.cs
│   │   ├── HomeViewModel.cs
│   │   ├── LoginViewModel.cs
│   │   ├── PagedResult.cs
│   │   └── ...
│   ├── Services/
│   │   └── ApiService.cs           # Typed HttpClient wrapper — GET / POST helpers with JWT header
│   ├── Views/                      # Razor views (one folder per controller)
│   ├── wwwroot/                    # Static assets (CSS, JS, images)
│   ├── appsettings.json            # ApiSettings:BaseUrl
│   └── Program.cs                  # DI: HttpClient, session, ApiService
│
├── DigiVault.Intranet/             # Internal admin panel
│   ├── Controllers/
│   │   ├── AccountController.cs    # Login / logout (JWT → session)
│   │   ├── DashboardController.cs
│   │   ├── UsersController.cs      # User list, detail, create, edit, ban/unban
│   │   ├── CoursesController.cs    # Course list, detail, soft delete
│   │   ├── CategoriesController.cs # Full CRUD for categories
│   │   ├── OrdersController.cs     # Order list, detail, cancel order / remove item
│   │   ├── NotificationsController.cs  # Notification list, send notification
│   │   ├── ReportsController.cs    # Report list, resolve report
│   │   └── SettingsController.cs   # Platform settings (commission rate)
│   ├── Views/
│   ├── wwwroot/
│   ├── appsettings.json
│   └── Program.cs
│
├── DigiVaultWeb.sln
└── global.json
```

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9)
- A running instance of the [DigiVault API](https://github.com/AIChelminska/DigiVaultAPI) (see its README for setup)

### Configuration

The portal needs to know where the API is. Open `DigiVault.PortalWWW/appsettings.json` and set `ApiSettings:BaseUrl`:

```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5052"
  }
}
```

> **Note:** Do not store secrets (API keys, passwords) in `appsettings.json`. Use `appsettings.Development.json` (excluded from git) or environment variables for anything sensitive.

| Key | Description |
|-----|-------------|
| `ApiSettings:BaseUrl` | Base URL of the DigiVaultAPI instance |

### Running Locally

```bash
# 1. Clone the repository
git clone https://github.com/your-org/DigiVaultWeb.git
cd DigiVaultWeb

# 2. Make sure the DigiVault API is running (see its README)

# 3. Run the public portal
dotnet run --project DigiVault.PortalWWW

# 4. Run the intranet (separate terminal)
dotnet run --project DigiVault.Intranet
```

Default ports (may vary by launch profile):

| App | URL |
|-----|-----|
| PortalWWW | `http://localhost:5000` |
| Intranet | `http://localhost:5001` |

> Check `Properties/launchSettings.json` in each project for the exact ports configured in your environment.

---

## Pages Overview

### PortalWWW

> Requires a valid session (JWT token obtained after login). All routes redirect to `/Account/Login` if the session is missing.

| Route | Description |
|-------|-------------|
| `GET /Account/Login` | Login form |
| `POST /Account/Login` | Authenticate via API → store JWT in session |
| `GET /Account/Logout` | Clear session and redirect to login |
| `GET /Home` | Home page — popular, newest, and top-rated course carousels |
| `GET /Courses` | Paginated course catalogue with search, category filter, and sort |
| `GET /Courses/Detail/{id}` | Course detail with reviews |
| `GET /Cart` | Shopping cart *(view stub — in progress)* |
| `GET /Wishlist` | Wishlist *(view stub — in progress)* |
| `GET /MyVault` | Purchased courses *(view stub — in progress)* |

### Intranet

> Requires login with a `Worker` (admin) account. All routes redirect to `/Account/Login` if the session is missing.
>
> **Live:** [https://digivaultintranet.onrender.com](https://digivaultintranet.onrender.com) — log in with `admin` / `admin`

| Route | Description |
|-------|-------------|
| `GET /Account/Login` | Admin login form |
| `POST /Account/Login` | Authenticate via API → store JWT in session |
| `GET /Account/Logout` | Clear session and redirect to login |
| `GET /Dashboard` | Admin dashboard |
| `GET /Categories` | Category list |
| `POST /Categories/Create` | Create a new category (modal) |
| `POST /Categories/Edit/{id}` | Update a category (modal) |
| `POST /Categories/Delete/{id}` | Delete a category |
| `GET /Users` | Paginated user list with search |
| `GET /Users/Detail/{id}` | User detail — profile, created and purchased courses |
| `POST /Users/Create` | Create a new user account (modal) |
| `POST /Users/Edit/{id}` | Update user role and status (modal) |
| `POST /Users/SetActive/{id}` | Activate a user |
| `POST /Users/SetNotActive/{id}` | Deactivate (ban) a user |
| `GET /Courses` | Paginated course list including hidden courses |
| `GET /Courses/Detail/{id}` | Course detail with reports count |
| `POST /Courses/Delete/{id}` | Soft-delete a course |
| `GET /Orders` | Paginated order list with date filter |
| `GET /Orders/Detail/{id}` | Order detail with item list |
| `POST /Orders/Delete/{id}` | Cancel an order and refund the user |
| `POST /Orders/RemoveItem/{orderId}/{courseId}` | Remove a single item from an order |
| `GET /Notifications` | Notification list |
| `POST /Notifications/Send` | Send a notification to a user (modal) |
| `GET /Reports` | Report list |
| `POST /Reports/Resolve/{id}` | Mark a report as resolved |
| `GET /Settings` | Platform settings (commission rate) |
| `POST /Settings/UpdateCommission` | Update the platform commission rate |

---

## Authentication

The portal uses **session-based authentication** backed by JWT:

1. The user submits credentials on the login form.
2. `AccountController` calls `POST /api/auth/login` on the DigiVaultAPI.
3. The returned JWT token is stored in the server-side session (`HttpContext.Session`).
4. `ApiService` reads the token from the session and attaches it as `Authorization: Bearer <token>` on every outgoing API request.
5. The user's first name is decoded directly from the JWT claims for display in the UI.

No cookies carry the raw JWT — the token lives exclusively in the server-side session.

### Test Accounts

#### PortalWWW (customer portal)

| Login | Password | Role | Notes |
|-------|----------|------|-------|
| `test` | `test` | User | Has balance, orders, and purchased courses |
| `test2` | `test` | User | |
| `admin` | `admin` | Worker | Can also log in to the portal |

#### Intranet (admin panel)

| Login | Password | Role |
|-------|----------|------|
| `admin` | `admin` | Worker (Admin) |

> Only accounts with the `Worker` role can access the Intranet. Logging in with a `User` account will result in a 403 error from the API.
>
> See the [DigiVault API README](https://github.com/AIChelminska/DigiVaultAPI#seeded-data) for the full list of seeded accounts.

---

## License

This project is open-source and available under the [MIT License](LICENSE).
