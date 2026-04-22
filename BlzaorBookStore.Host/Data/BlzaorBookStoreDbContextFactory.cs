using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlzaorBookStore.Data;

public class BlzaorBookStoreDbContextFactory : IDesignTimeDbContextFactory<BlzaorBookStoreDbContext>
{
    public BlzaorBookStoreDbContext CreateDbContext(string[] args)
    {
        BlzaorBookStoreGlobalFeatureConfigurator.Configure();
        BlzaorBookStoreModuleExtensionConfigurator.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<BlzaorBookStoreDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new BlzaorBookStoreDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
