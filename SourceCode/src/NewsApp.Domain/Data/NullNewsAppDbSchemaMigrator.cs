using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace NewsApp.Data;

/* This is used if database provider does't define
 * INewsAppDbSchemaMigrator implementation.
 */
public class NullNewsAppDbSchemaMigrator : INewsAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
