using Microsoft.EntityFrameworkCore;
using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Persistence;
using Registration.Persistence.Data;
using Registration.Persistence.Repositories;
using Registration.Services.Registration;
using Registration.Services.Users;

namespace Registration.Api.Extensions;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add repositories.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }

    /// <summary>
    /// Add unit of work.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    /// <summary>
    /// Add database.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="dbProviderType">Database provider type.</param>
    /// <param name="connectionString">Connection string.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services, DbProviderType dbProviderType, string connectionString)
    {
        var optionsAction = GetOptionsAction(dbProviderType, connectionString);
        services.AddDbContext<RegistrationContext>(optionsAction);

        return services;
    }

    /// <summary>
    /// Add bussiness services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<RegistrationService>();

        return services;
    }

    #region private
    /// <summary>
    /// Get options action.
    /// </summary>
    /// <param name="dbProviderType">Database provider type.</param>
    /// <param name="connectionString">Connection string.</param>
    /// <returns>Options action.</returns>
    private static Action<DbContextOptionsBuilder>? GetOptionsAction(DbProviderType dbProviderType, string connectionString)
    {
        switch (dbProviderType)
        {
            case DbProviderType.InMemory:
                return (DbContextOptionsBuilder options) => options.UseInMemoryDatabase(connectionString);
            case DbProviderType.SqlServer:
                return (DbContextOptionsBuilder options) => options.UseSqlServer(connectionString);
            case DbProviderType.PostgreSql:
                return (DbContextOptionsBuilder options) => options.UseNpgsql(connectionString);
            default:
                return null;
        }
    }

    #endregion
}
