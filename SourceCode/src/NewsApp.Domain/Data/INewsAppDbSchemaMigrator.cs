using System.Threading.Tasks;

namespace NewsApp.Data;

public interface INewsAppDbSchemaMigrator
{
    Task MigrateAsync();
}
