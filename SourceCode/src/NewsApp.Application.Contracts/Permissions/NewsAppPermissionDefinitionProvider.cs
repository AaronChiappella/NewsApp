using NewsApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace NewsApp.Permissions;

public class NewsAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(NewsAppPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(NewsAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NewsAppResource>(name);
    }
}
