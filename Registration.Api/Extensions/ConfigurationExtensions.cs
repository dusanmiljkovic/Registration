using Microsoft.EntityFrameworkCore;

namespace Registration.Api.Extensions;

/// <summary>
/// Configuration extensions. 
/// </summary>
public static class ConfigurationExtensions
{
    private const string InMemory = "InMemoryDbConnection";
    private const string SqlServer = "SqlServerDbConnection";

    /// <summary>
    /// Get database provider type.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    /// <returns>database provider type.</returns>
    public static DbProviderType GetDbProviderType(this IConfiguration configuration)
    {
        string connectionStringName = configuration["DbProvider:ConnectionStringName"];
        return GetDbProviderType(connectionStringName);
    }

    /// <summary>
    /// Get connection string.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    /// <param name="dbProviderType">Database provider type.</param>
    /// <returns>Connection string.</returns>
    public static string GetConnectionString(this IConfiguration configuration, DbProviderType dbProviderType)
    {
        var name = GetConnectionStringName(dbProviderType);
        return configuration.GetConnectionString(name);
    }

    /// <summary>
    /// Get database provider type.
    /// </summary>
    /// <param name="connectionStringName">Connection string.</param>
    /// <returns>Database provider type.</returns>
    private static DbProviderType GetDbProviderType(string connectionStringName) => connectionStringName switch
    {
        SqlServer => DbProviderType.SqlServer,
        _ => DbProviderType.InMemory,
    };

    /// <summary>
    /// Get connection string.
    /// </summary>
    /// <param name="dbProviderType">Database provider type.</param>
    /// <returns>Connection string.</returns>
    private static string GetConnectionStringName(DbProviderType dbProviderType) => dbProviderType switch
    {
        DbProviderType.SqlServer => SqlServer,
        _ => InMemory,
    };
}
