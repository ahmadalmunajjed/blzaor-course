using Volo.Abp.Application.Services;
using BlzaorBookStore.Localization;

namespace BlzaorBookStore.Services;

/* Inherit your application services from this class. */
public abstract class BlzaorBookStoreAppService : ApplicationService
{
    protected BlzaorBookStoreAppService()
    {
        LocalizationResource = typeof(BlzaorBookStoreResource);
    }
}