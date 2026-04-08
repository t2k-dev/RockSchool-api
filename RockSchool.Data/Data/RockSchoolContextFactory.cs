using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RockSchool.Data.Data;

public class RockSchoolContextFactory : IDesignTimeDbContextFactory<RockSchoolContext>
{
    public RockSchoolContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RockSchoolContext>();

        optionsBuilder.UseNpgsql(ResolveConnectionString());

        return new RockSchoolContext(optionsBuilder.Options);
    }

    private static string ResolveConnectionString()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                              ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                              ?? "Development";

        var envConnectionString = Environment.GetEnvironmentVariable("DbContextSettings__ConnectionString");
        if (!string.IsNullOrWhiteSpace(envConnectionString))
        {
            return envConnectionString;
        }

        var basePaths = new[]
        {
            Directory.GetCurrentDirectory(),
            Path.Combine(Directory.GetCurrentDirectory(), "RockSchool.WebApi"),
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "RockSchool.WebApi"))
        };

        foreach (var basePath in basePaths.Distinct())
        {
            var connectionString = TryReadConnectionString(basePath, environmentName);
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                return connectionString;
            }
        }

        throw new InvalidOperationException(
            "Unable to resolve 'DbContextSettings:ConnectionString' for RockSchoolContext design-time factory.");
    }

    private static string? TryReadConnectionString(string basePath, string environmentName)
    {
        if (!Directory.Exists(basePath))
        {
            return null;
        }

        var connectionString = TryReadConnectionStringFromFile(Path.Combine(basePath, "appsettings.json"));
        var environmentConnectionString = TryReadConnectionStringFromFile(
            Path.Combine(basePath, $"appsettings.{environmentName}.json"));

        return environmentConnectionString ?? connectionString;
    }

    private static string? TryReadConnectionStringFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        var json = File.ReadAllText(filePath);
        using var document = JsonDocument.Parse(json, new JsonDocumentOptions
        {
            CommentHandling = JsonCommentHandling.Skip
        });

        if (!document.RootElement.TryGetProperty("DbContextSettings", out var dbContextSettings))
        {
            return null;
        }

        if (!dbContextSettings.TryGetProperty("ConnectionString", out var connectionStringElement))
        {
            return null;
        }

        return connectionStringElement.GetString();
    }
}
