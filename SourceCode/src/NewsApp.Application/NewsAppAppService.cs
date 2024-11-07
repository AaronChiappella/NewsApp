using System;
using System.Collections.Generic;
using System.Text;
using NewsApp.Localization;
using Volo.Abp.Application.Services;

namespace NewsApp;

/* Inherit your application services from this class.
 */
public abstract class NewsAppAppService : ApplicationService
{
    protected NewsAppAppService()
    {
        LocalizationResource = typeof(NewsAppResource);
    }
}
