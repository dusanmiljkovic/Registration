# Registration projet

The goal of this project is implement DDD principles with .NET Core 6.0.

## Model
Model consists of two entites: User and Company.

### Data model entities
There are two entities: 
- `User` 
- `Company` 

Both entities derive from `BaseEntity` base class.

### Database engines
There are three database providers implemented:

- In-memory database - `DbProviderType.InMemory`
- Microsoft SQL Server - `DbProviderType.SqlServer`
- PostgreSQL - `DbProviderType.PostgreSql`

Choose database provider in Registration.Api/appsettings.json by setting appropriate value for key DbProvider:ConnectionStringName:

- *InMemoryDbConnection* value for in-memory database
- *SqlServerDbConnection* value for Microsoft SQL Server
- *PostgreSqlDbConnection* value for PostgreSQL
> **Note:** It's possible that connection string needs to be changed to so it is in compliance with your environment.

### Entity configuration
Every entity has its own EF configuration class that implements `IEntityTypeConfiguration<TEntity>` interface. Configuration classes have the same name as corresponding entity with *Configuration* suffix, e.g. `UserConfiguration`, `CompanyConfiguration`.

### Migrations
When switching between MySQL and PostgreSQL database it's neccessary to remove migrations and generate new ones based on the database engine you want to working with.


## Technologies implemented:
- ASP.NET Core 6.0
- ASP.NET MVC Core
- Entity Framework Core 6.0.1
- Swagger UI

## Architecture:
- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Validations
- CQRS
- Unit of Work
- Repository

## Possible improvements
Add docker files and images so that code can run in any environment.
