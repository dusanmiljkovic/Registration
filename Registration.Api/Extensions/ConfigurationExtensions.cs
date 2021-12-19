using Registration.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Registration.Api.Extensions;
public static class ConfigurationExtensions
{
    private const string InMemory = "InMemoryDbConnection";
    private const string SqlServer = "SqlServerDbConnection";

    public static DbProviderType GetDbProviderType(this IConfiguration configuration)
    {
        string connectionStringName = configuration["DbProvider:ConnectionStringName"];
        return GetDbProviderType(connectionStringName);
    }
    public static string GetConnectionString(this IConfiguration configuration, DbProviderType dbProviderType)
    {
        var name = GetConnectionStringName(dbProviderType);
        return configuration.GetConnectionString(name);
    }

    private static DbProviderType GetDbProviderType(string connectionStringName) => connectionStringName switch
    {
        SqlServer => DbProviderType.SqlServer,
        _ => DbProviderType.InMemory,
    };

    private static string GetConnectionStringName(DbProviderType dbProviderType) => dbProviderType switch
    {
        DbProviderType.SqlServer => SqlServer,
        _ => InMemory,
    };
}
