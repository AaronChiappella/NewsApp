using Volo.Abp.Modularity;

namespace NewsApp;

[DependsOn(
    typeof(NewsAppApplicationModule),
    typeof(NewsAppDomainTestModule)
    )]
public class NewsAppApplicationTestModule : AbpModule
{

}
