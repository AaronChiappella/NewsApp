using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace NewsApp;

[Dependency(ReplaceServices = true)]
public class NewsAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "NewsApp";
}
