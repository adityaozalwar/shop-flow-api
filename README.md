# ShopFlow

A simple e-commerce order management REST API built with **ASP.NET Core**, **Repository Pattern**, and **EF Core**.

Customers browse products, add to cart, and place orders. Admins manage products and update order status.

## Features

- Register and login with JWT (Admin and Customer roles)
- Product and category management
- Search, filter, and pagination
- Cart and place order (inside a database transaction)
- Order status flow: Placed → Paid → Shipped → Delivered
- Global error handling, logging, and Swagger docs

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
dotnet ef database update
dotnet run --project ShopFlowAPI
```

Open the app at `http://localhost:7173/`.

## Roadmap

ShopFlow is a learning project that is growing step by step. Planned work:

- [ ] Move to Clean Architecture (Api, Application, Domain, Infrastructure)
- [ ] Unit and integration tests
- [ ] CI/CD (GitHub Actions)

Want to help with any of these? See the issues labeled `good first issue`.

## Contributing

Contributions are welcome! Fork the repo, create a branch, and open a pull request. See [CONTRIBUTING.md](CONTRIBUTING.md) for details.

## License

MIT
