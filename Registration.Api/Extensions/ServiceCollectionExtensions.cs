using Microsoft.EntityFrameworkCore;
using Registration.Api.Services.Users;
using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Persistence;
using Registration.Persistence.Data;
using Registration.Persistence.Repositories;
using Registration.Persistence.Repositories.Common;

namespace Registration.Api.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, DbProviderType dbProviderType, string connectionString)
    {
        var optionsAction = GetOptionsAction(dbProviderType, connectionString);
        services.AddDbContext<RegistrationContext>(optionsAction);

        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        return services;
    }

    #region private

    private static Action<DbContextOptionsBuilder>? GetOptionsAction(DbProviderType dbProviderType, string connectionString)
    {
        switch (dbProviderType)
        {
            case DbProviderType.InMemory:
                return (DbContextOptionsBuilder options) => options.UseInMemoryDatabase(connectionString);
            case DbProviderType.SqlServer:
                return (DbContextOptionsBuilder options) => options.UseSqlServer(connectionString);
            default:
                return null;
        }
    }

    #endregion
}
