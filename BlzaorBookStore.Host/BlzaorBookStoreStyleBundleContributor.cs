using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace BlzaorBookStore;

public class BlzaorBookStoreStyleBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add(new BundleFile("main.css", true));
    }
}