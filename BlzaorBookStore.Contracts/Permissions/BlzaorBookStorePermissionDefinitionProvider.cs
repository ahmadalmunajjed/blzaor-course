using BlzaorBookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace BlzaorBookStore.Permissions;

public class BlzaorBookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BlzaorBookStorePermissions.GroupName);


        var booksPermission = myGroup.AddPermission(BlzaorBookStorePermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(BlzaorBookStorePermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(BlzaorBookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(BlzaorBookStorePermissions.Books.Delete, L("Permission:Books.Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(BlzaorBookStorePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BlzaorBookStoreResource>(name);
    }
}
