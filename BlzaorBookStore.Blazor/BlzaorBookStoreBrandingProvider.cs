using Microsoft.Extensions.Localization;
using BlzaorBookStore.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace BlzaorBookStore;

[Dependency(ReplaceServices = true)]
public class BlzaorBookStoreBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<BlzaorBookStoreResource> _localizer;

    public BlzaorBookStoreBrandingProvider(IStringLocalizer<BlzaorBookStoreResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}