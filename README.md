# AccountsService
This app is dedicated for financial Account operations using API.

## Project structure
AccountsService.Api - API project.\
AccountsService.Core - project of services and repositories.\
AccountsService.Models - shared models between projects.\
AccountsService.Storage - EF entities, migrations, ApplicationDbContext.\
AccountsService.Tests - unit tests project.

## API documentation
Swagger

## Configurations
Database connection string is in appsettings.json {"ConnectionStrings":"DatabaseConnection"}

## Project startup
Database can be migrated from Package manage console, or when AccountsService.Api is launched.

### Startup
Open a terminal/command prompt and navigate to the folder in which you keep AccountsService.Api project\
Write command dotnet run
