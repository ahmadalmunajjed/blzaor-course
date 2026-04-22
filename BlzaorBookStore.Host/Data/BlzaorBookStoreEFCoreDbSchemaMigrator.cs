using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BlzaorBookStore.Data;

public class BlzaorBookStoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public BlzaorBookStoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the BlzaorBookStoreDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BlzaorBookStoreDbContext>()
            .Database
            .MigrateAsync();

    }
}
