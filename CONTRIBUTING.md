# Contributing to ShopFlow

Thanks for your interest in helping! All contributions are welcome, from fixing a typo to adding a feature.

## Getting Started

1. **Fork** the repo and clone your fork.
2. Follow the steps in the [README](README.md) to run the project locally.
3. Pick an issue labeled `good first issue`, and comment on it so others know you are working on it.

## Making Changes

1. Create a branch from `main`:
   ```bash
   git checkout -b feature/short-description
   ```
2. Make your changes. Keep them small and focused on one thing.
3. Make sure the project builds:
   ```bash
   dotnet build
   ```
4. Commit with a clear message, for example `Add health check endpoint`.
5. Push your branch and open a **pull request** to `main`.

## Guidelines

- Follow the existing code style and naming.
- Do not commit `bin/`, `obj/`, or secrets (connection strings, JWT keys).
- Add or update tests when you change behavior.
- Keep pull requests small. One change per PR is easier to review.
- Be kind and respectful in issues and reviews.

## Reporting Bugs or Suggesting Features

Open an issue and include:
- What you expected and what happened
- Steps to reproduce (for bugs)
- Your .NET version and operating system

## Questions?

Open an issue with the `question` label. No question is too small.
