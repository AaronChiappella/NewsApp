using NewsApp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace NewsApp;

[DependsOn(
    typeof(NewsAppEntityFrameworkCoreTestModule)
    )]
public class NewsAppDomainTestModule : AbpModule
{

}
