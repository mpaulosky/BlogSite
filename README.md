# BlogSite Solution

## New Aspire and BlazorServer Hosted Application

### A tool to Create and Manage Articles using a PostgresSql to store documents. It includes architecture, bunit, unit and integration tests with the integration tests using a docker container for the test PostgresSql database to ensure clean isolated data for the tests

****
![GitHub](https://img.shields.io/github/license/mpaulosky/BlogSite?logo=github)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/mpaulosky/BlogSite?logo=github)
[![CodeCov Main](https://codecov.io/gh/mpaulosky/BlogSite/branch/main/graph/badge.svg)](https://codecov.io/gh/mpaulosky/BlogSite)
****
[![.NET Build](https://github.com/mpaulosky/BlogSite/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mpaulosky/BlogSite/actions/workflows/dotnet.yml)
****
[![Open Issues](https://img.shields.io/github/issues/mpaulosky/BlogSite.svg?style=flatsquare&logo=github&label=Open%20Issues)](https://github.com/mpaulosky/BlogSite/issues)
[![Closed Issues](https://img.shields.io/github/issues-closed/mpaulosky/BlogSite.svg?style=flatsquare&logo=github&label=Closed%20Issues)](https://github.com/mpaulosky/BlogSite/issues?q=sort%3Aupdated-desc+is%3Aissue+is%3Aclosed)
[![Open Bug Issues](https://img.shields.io/github/issues/mpaulosky/BlogSite/bug.svg?style=flatsquare&logo=github&label=Open%20Bug%20Issues)](https://github.com/mpaulosky/BlogSite/issues?q=is%3Aissue+is%3Aopen+label%3Abug)
[![Closed Bug Issues](https://img.shields.io/github/issues-closed/mpaulosky/BlogSite/bug.svg?style=flatsquare&logo=github&label=Closed%20Bug%20Issues)](https://github.com/mpaulosky/BlogSite/issues?q=is%3Aissue+is%3Aclosed+label%3Abug)
****
![GitHub pull requests](https://img.shields.io/github/issues-pr/mpaulosky/BlogSite?label=pull%20requests&logo=github)
![GitHub closed pull requests](https://img.shields.io/github/issues-pr-closed/mpaulosky/BlogSite?logo=github)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/mpaulosky/BlogSite/main?label=last%20commit%20main&logo=github)
****

## Overview

BlazorApp is a modern, secure, and scalable .NET 9 solution built with Blazor Server, .NET Aspire, and PostgresSQL. It
demonstrates best practices in architecture, testing, and cloud-native development, including CQRS, Vertical Slice, and
strong security defaults (HTTPS, CORS, Antiforgery, secure headers, and pluggable authentication providers).

****

## Solution Structure

```
Web               -- Blazor Server UI (main entrypoint, interactive server rendering)
AppHost           -- Aspire App Host (orchestration, resource wiring, environment config)
ServiceDefaults   -- Shared service defaults (OpenTelemetry, health checks, DI, resilience)
Shared            -- Shared contracts, constants, and service/resource names
tests             -- Unit, integration, and architecture tests (xUnit, bUnit, Playwright)
```

****

## Key Technologies & Features

- **.NET 9** & **.NET Aspire** (cloud-native orchestration)
- **Blazor Server** (interactive, stream rendering, error boundaries)
- **Authentication (ASP.NET Core authentication providers)
- **CQRS & Vertical Slice Architecture**
- **Dependency Injection** everywhere
- **OpenTelemetry & Application Insights**
- **Distributed Caching** (Redis)
- **Output Caching**
- **Health Checks**
- **FluentValidation** for model validation
- **Unit, Integration, and Architecture Tests** (xUnit, bUnit, Playwright, TestContainers)

****

## Coding & Architecture Standards

This repository enforces the following rules for all .NET code (see `.editorconfig`, StyleCop, and tooling):

- **Modifier Order:** `public`, `private`, `protected`, `internal`, `static`, `readonly`, `const`
- **Explicit Types:** Use explicit types except where `var` improves clarity
- **Null Checks:** Use `is null` / `is not null`
- **Records & Minimal APIs:** Prefer records and minimal APIs
- **File Scoped Namespaces & Global Usings:** Use file-scoped namespaces and centralized global usings (
  `GlobalUsings.cs`)
- **Nullable Reference Types:** Enabled
- **Pattern Matching:** Preferred
- **Line Length:** Max 120
- **Tabs:** Indent size 2
- **LF Endings, UTF-8 Charset, Final Newline, Trim Trailing Whitespace**

### Naming

- **Interfaces:** Prefix `I` (e.g., `IService`)
- **Async Methods:** Suffix `Async`
- **Private Fields:** Prefix `_`
- **Constants:** UPPER_CASE
- **Blazor:** Suffix `Component` (for components), `Page` (for pages)

### Security

- **HTTPS, Authentication, Authorization, Antiforgery, CORS, Secure Headers**  
  See `Web/Program.cs` for implementation.

### Architecture

- **SOLID, Dependency Injection, Async/Await, Strongly Typed Config, CQRS, Vertical Slice, Aspire**
- **Centralized NuGet Package Versions:** All versions managed in `Directory.Packages.props` at repo root.
- **Unit, Integration, Architecture Tests:** See `tests/` and `tests/Architecture.Tests.Unit/`

### Blazor

- **State Management, Interactive Server Rendering, Stream Rendering, Virtualization, Error Boundaries**
- **Component Lifecycle, Cascading Parameters, Render Fragments**
- See `Web/`, `Web.Virtualization/`, and `MainLayout.razor`

### Documentation

- **XML Docs, Swagger/OpenAPI, Component Docs, README, CONTRIBUTING.md, LICENSE, Code of Conduct**
- See `docs/README.md`, `docs/CONTRIBUTING.md`, `LICENSE`, `CODE_OF_CONDUCT.md`

### Logging & Monitoring

- **Structured Logging, Health Checks, OpenTelemetry, Application Insights**

### Database

- **Entity Framework Core, MongoDB (see `Persistence.PostgresSql/`), SQL Server**
- **Async Operations, TestContainers for Integration
  Testing (`tests/Article Service.Persistence.PostgresSql.Tests.Integration/`), Change Tracking, DbContext Pooling**

### Versioning & Caching

- **API Versioning, Semantic Versioning**
- **Distributed Cache, Output Caching (`Web/Program.cs`)**

### Middleware

- **Exception Handling, Request Logging, Cross-Cutting Concerns**

### Background Services

- **Background Service Required**

### Environment

- **Environment Config, User Secrets, Key Vault**

### Validation

- **Model Validation, FluentValidation**

### Testing

- **Unit, Integration, Architecture Tests**
- **xUnit, FluentAssertions, NSubstitute, Moq, bUnit (`tests/Web.Tests.Unit/`), Playwright**

****

## Getting Started

### Authentication

This application supports ASP.NET Core authentication providers. Configure authentication via user secrets or
environment variables in the `AppHost` project and ensure the provider-specific settings are present before running the
application.

### Running the Application

1. **Requirements:** .NET 9 SDK, Docker (for MongoDB/Redis/TestContainers), Node.js (for Playwright tests)
2. **Run the App:**
  - `dotnet run --project AppHost` (or use Visual Studio/Rider launch)
3. **Browse:** Navigate to the provided endpoint (see console output)
4. **tests:**
  - `dotnet test` (runs all unit/integration/architecture tests)

****

## Contribution & Documentation

- [Code of Conduct](./docs/CODE_OF_CONDUCT.md)
- [Contributing Guide](./docs/CONTRIBUTING.md)
- [Architecture & Usage Docs](./README.md)

****

## Software References

- .NET 9, .NET Aspire
- Blazor Server, C#, TailwindCSS
- PostgresSQL, Redis

****

## License

See [LICENSE](./LICENSE.txt)
