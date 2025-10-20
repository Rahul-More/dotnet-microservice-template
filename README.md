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

## Customization

- Extend entities/services as needed
- Plug in additional providers via DI
