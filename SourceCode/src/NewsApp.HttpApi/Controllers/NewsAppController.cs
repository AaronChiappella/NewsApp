using NewsApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace NewsApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class NewsAppController : AbpControllerBase
{
    protected NewsAppController()
    {
        LocalizationResource = typeof(NewsAppResource);
    }
}
