# .NET 9 Microservice Template

A starter template for modern .NET microservices:
- ASP.NET Core 9 REST API
- OpenAPI/Swagger
- Fusion Memory Cache and Redis
- Serilog logging with Logstash/Kibana config
- Docker/Docker Compose
- Dapper & EF Core persistence (code-first)
- Domain-driven design & flexible DB integration (Postgres/MongoDB) with consistent hashing
- Example service and repository layers

## Getting Started

1. Clone the repo
2. Build and run with Docker Compose:
   ```
docker-compose up --build
   ```
3. API available at `http://localhost:8080/swagger`

## Configuration

See `src/Api/appsettings.json` for connection strings and logging.

## Structure

- `src/Api`: API project
- `src/Domain`: Entities, Services, Repositories
- `src/Persistence`: Database providers, DbContext

## Scripted project scaffold

A PowerShell script is included to quickly generate a simple solution skeleton (solution + API + Application + Infrastructure + Domain + Tests), add project references, add EF Core & Npgsql packages to the Infrastructure project, create a Dockerfile for the API, and add a GitHub Actions workflow.

- Script path: `script/create-project.ps1`
- What it does:
  - Creates a solution folder and .sln
  - Creates projects: API (webapi), Application (classlib), Infrastructure (classlib), Domain (classlib), Tests (xunit)
  - Adds projects to the solution and wires project references
  - Installs EF Core and Npgsql packages into the Infrastructure project
  - Generates a Dockerfile under the API project
  - Creates `.github/workflows/dotnet.yml` GitHub Actions workflow for build/test/publish and Docker build

Usage:

```powershell
# From repository root (PowerShell / pwsh)
pwsh ./script/create-project.ps1
```

Requirements:
- .NET SDK 8.0 (for templates and build)
- PowerShell 7+ (or Windows PowerShell with appropriate privileges)
- Docker (optional — for Dockerfile and building images)
- GitHub Actions runner (for CI workflow — runs on github.com)

Customize:
- Edit the `$projectName` variable at the top of `script/create-project.ps1` to change the project naming.
- Tweak the Dockerfile, workflow, and package choices as needed.

## Customization

- Extend entities/services as needed
- Plug in additional providers via DI
