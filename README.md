# ShopFlow

A simple e-commerce order management REST API built with **ASP.NET Core**, **Repository Pattern**, and **EF Core**.

Customers browse products, add to cart, and place orders. Admins manage products and update order status.

**Live API:** [https://shopflow-api-wrgk.onrender.com/](https://shopflow-api-wrgk.onrender.com/)

**Swagger docs:** [https://shopflow-api-wrgk.onrender.com/swagger](https://shopflow-api-wrgk.onrender.com/swagger)

Demo : **`demouser@shopflow.com`**

Password : Demo@123
 
> Hosted on Render's free tier. If the first request is slow, the service may be waking up; a cron job pings it regularly to keep it alive.


## Features

- Register and login with JWT (Admin and Customer roles)
- Product and category management
- Search, filter, and pagination
- Cart and place order (inside a database transaction)
- Order status flow: Placed → Paid → Shipped → Delivered
- Global error handling, logging, and Swagger docs

## Tech Stack

| Layer            | Technology                    |
| ---------------- | ----------------------------- |
| Framework        | .NET, ASP.NET Core Web API    |
| ORM              | Entity Framework Core         |
| Database         | PostgreSQL                    |
| Auth             | JWT (role-based)              |
| Pattern          | Repository + Service layers   |
| API docs         | Swagger / OpenAPI             |

## Project Structure

```
ShopFlowAPI/
├── Controllers/    # HTTP endpoints
├── Services/       # Business logic
├── Repository/     # Data access (Repository Pattern)
├── Models/         # EF Core entities
├── Data/           # DbContext
├── Migrations/     # EF Core migrations
├── Enums/          # Roles, order status, etc.
├── Mappers/        # Entity <-> DTO mapping
├── Middlewares/    # Global exception handling
├── Exceptions/     # Custom exceptions
└── Program.cs      # App startup and DI configuration
```

## Database Design (ER Diagram)

<img width="522" height="400" alt="Screenshot 2026-10-14 at 23 02 06" src="https://github.com/user-attachments/assets/77ee5d35-90b7-4135-b6fb-1b7d1c708d52" />


### Relationships

- A **User** has one **Cart** and can place many **Orders**.
- A **Category** groups many **Products**.
- A **Cart** holds many **CartItems**, each pointing to one **Product**.
- An **Order** contains one or more **OrderItems**, each pointing to one **Product**.
- `OrderItem.UnitPrice` stores the price at purchase time so later product price changes do not alter past orders.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### Setup

```bash
git clone https://github.com/adityaozalwar/shop-flow-api.git
cd shop-flow-api
dotnet restore
```

Set your PostgreSQL connection string in `appsettings.Development.json`, then run:

```bash
dotnet ef database update
dotnet run
```

Open the app at `http://localhost:7173/` (Swagger UI is available for exploring the endpoints).

## Roadmap

ShopFlow is a learning project that is growing step by step. Planned work:

- [ ] Move to Clean Architecture (Api, Application, Domain, Infrastructure)
- [ ] Unit and integration tests
- [ ] CI/CD (GitHub Actions)

Want to help with any of these? See the issues tab.

## Contributing

Contributions are welcome! Fork the repo, create a branch, and open a pull request. See [CONTRIBUTING.md](CONTRIBUTING.md) for details.

## License

MIT
