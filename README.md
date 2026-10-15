# ShopFlow

A simple e-commerce order management REST API built with **ASP.NET Core**, **Clean Architecture**, and **EF Core**.

Customers browse products, add to cart, and place orders. Admins manage products and update order status.

## Features

- Register and login with JWT (Admin and Customer roles)
- Product and category management
- Search, filter, and pagination
- Cart and place order (inside a database transaction)
- Order status flow: Placed → Paid → Shipped → Delivered
- Global error handling, logging, and Swagger docs

## Architecture

The solution is split into four projects:

- `Domain` contains the business entities and domain enums.
- `Application` contains use cases, service and repository contracts, DTOs, and mapping.
- `Infrastructure` contains EF Core persistence, repositories, Identity/JWT implementation, and database seeding.
- `ShopFlowAPI` is the HTTP API host and composition root.

Dependencies point inward: Infrastructure implements Application contracts, while the API host wires the layers together.

## Tech Stack

.NET, ASP.NET Core Web API, EF Core, PostgreSQL

## Getting Started

```bash
git clone https://github.com/adityaozalwar/shopflow-api.git
cd shopflow-api
dotnet restore
```

Set your PostgreSQL connection string in `appsettings.Development.json`, then run:

```bash
dotnet ef database update --project Infrastructure/ShopFlowAPI.Infrastructure.csproj --startup-project ShopFlowAPI.csproj
dotnet run --project ShopFlowAPI
```

Open the app at `http://localhost:7173/`.

## Roadmap

ShopFlow is a learning project that is growing step by step. Planned work:

- [ ] Unit and integration tests
- [ ] CI/CD (GitHub Actions)

Want to help with any of these? See the issues labeled `good first issue`.

## Contributing

Contributions are welcome! Fork the repo, create a branch, and open a pull request. See [CONTRIBUTING.md](CONTRIBUTING.md) for details.

## License

MIT
