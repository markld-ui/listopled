namespace Listopled.Infrastructure;

using System.Globalization;
using Listopled.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = ResolvePostgresConnectionString(configuration);

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    private static string ResolvePostgresConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (!string.IsNullOrWhiteSpace(connectionString)
            && !string.Equals(connectionString, "ConfiguredByEnvironment", StringComparison.OrdinalIgnoreCase))
        {
            return connectionString;
        }

        var host = configuration["DB_HOST"];
        var portValue = configuration["DB_PORT"];
        var database = configuration["DB_NAME"];
        var username = configuration["DB_USER"];
        var password = configuration["DB_PASSWORD"];

        if (string.IsNullOrWhiteSpace(host)
            || string.IsNullOrWhiteSpace(portValue)
            || string.IsNullOrWhiteSpace(database)
            || string.IsNullOrWhiteSpace(username)
            || string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                "PostgreSQL connection must be configured through ConnectionStrings:DefaultConnection or DB_* environment variables.");
        }

        if (!int.TryParse(portValue, NumberStyles.None, CultureInfo.InvariantCulture, out var port))
        {
            throw new InvalidOperationException("DB_PORT must be a valid integer.");
        }

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = port,
            Database = database,
            Username = username,
            Password = password
        };

        return builder.ConnectionString;
    }
}
