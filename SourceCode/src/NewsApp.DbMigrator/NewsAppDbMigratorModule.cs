using NewsApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace NewsApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(NewsAppEntityFrameworkCoreModule),
    typeof(NewsAppApplicationContractsModule)
    )]
public class NewsAppDbMigratorModule : AbpModule
{
}
