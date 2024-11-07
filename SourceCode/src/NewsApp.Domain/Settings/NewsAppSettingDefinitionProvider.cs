using Volo.Abp.Settings;

namespace NewsApp.Settings;

public class NewsAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(NewsAppSettings.MySetting1));
    }
}
