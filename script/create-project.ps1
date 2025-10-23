# Set your project name
$projectName = "ProductService"

# Define project folders
$solutionFolder = "$projectName"
$apiProject = "$projectName.API"
$appProject = "$projectName.Application"
$infraProject = "$projectName.Infrastructure"
$domainProject = "$projectName.Domain"
$testProject = "$projectName.Tests"

# Create solution foldera
New-Item -ItemType Directory -Path $solutionFolder
Set-Location $solutionFolder

# Create solution
dotnet new sln -n $projectName

# Create projects
dotnet new webapi -n $apiProject
dotnet new classlib -n $appProject
dotnet new classlib -n $infraProject
dotnet new classlib -n $domainProject
dotnet new xunit -n $testProject

# Add projects to solution
dotnet sln add "$apiProject/$apiProject.csproj"
dotnet sln add "$appProject/$appProject.csproj"
dotnet sln add "$infraProject/$infraProject.csproj"
dotnet sln add "$domainProject/$domainProject.csproj"
dotnet sln add "$testProject/$testProject.csproj"

# Add project references
dotnet add "$apiProject/$apiProject.csproj" reference "$appProject/$appProject.csproj"
dotnet add "$apiProject/$apiProject.csproj" reference "$infraProject/$infraProject.csproj"
dotnet add "$appProject/$appProject.csproj" reference "$domainProject/$domainProject.csproj"
dotnet add "$infraProject/$infraProject.csproj" reference "$domainProject/$domainProject.csproj"
dotnet add "$testProject/$testProject.csproj" reference "$appProject/$appProject.csproj"
dotnet add "$testProject/$testProject.csproj" reference "$infraProject/$infraProject.csproj"

# Add EF Core and PostgreSQL packages
dotnet add "$infraProject/$infraProject.csproj" package Microsoft.EntityFrameworkCore
dotnet add "$infraProject/$infraProject.csproj" package Microsoft.EntityFrameworkCore.Design
dotnet add "$infraProject/$infraProject.csproj" package Npgsql.EntityFrameworkCore.PostgreSQL

# Create Dockerfile in API project
@"
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "$apiProject/$apiProject.csproj"
RUN dotnet build "$apiProject/$apiProject.csproj" -c Release -o /app/build
RUN dotnet publish "$apiProject/$apiProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "$apiProject.dll"]
"@ | Set-Content "$apiProject/Dockerfile"

# Create GitHub Actions workflow
New-Item -ItemType Directory -Path ".github/workflows" -Force
@"
name: Build and Deploy

on:
  push:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal
    - name: Publish
      run: dotnet publish "$apiProject/$apiProject.csproj" -c Release -o out
    - name: Docker Build
      run: docker build -t $projectName-api -f "$apiProject/Dockerfile" .
"@ | Set-Content ".github/workflows/dotnet.yml"

Write-Host "✅ Microservice solution scaffolded with EF Core, Docker, and GitHub Actions!"