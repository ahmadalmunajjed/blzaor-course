using BlzaorBookStore.Localization;
using Volo.Abp.AspNetCore.Components;

namespace BlzaorBookStore;

public abstract class BlzaorBookStoreComponentBase : AbpComponentBase
{
    protected BlzaorBookStoreComponentBase()
    {
        LocalizationResource = typeof(BlzaorBookStoreResource);
    }
}
