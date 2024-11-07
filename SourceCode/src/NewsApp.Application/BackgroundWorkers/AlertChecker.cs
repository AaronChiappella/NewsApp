using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewsApp.AlertsSearches;
using NewsApp.News;
using NewsApp.Notifications;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Threading;
using Volo.Abp.Users;

namespace NewsApp.BackgroundServices
{
    public class AlertChecker : AsyncPeriodicBackgroundWorkerBase
    {
        public AlertChecker(                
                AbpAsyncTimer timer,
                IServiceScopeFactory serviceScopeFactory
                ) : base(
                timer,
                serviceScopeFactory)
        {
            Timer.Period = 1 * 60 * 1000 / 10; // en milisegundos
        }

        protected async override Task DoWorkAsync(
            PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogInformation("Checking if there are new results for the alerts of the current user...");

            //Resolve dependencies
            var alertAppService = workerContext.ServiceProvider.GetRequiredService<IAlertSearchAppService>();

            //Do the work
            await alertAppService.NotifyUserOfNewResultsAsync();
            Logger.LogInformation("AlertChecker has completed its work...");
        }

        public async Task DoWorkAccesibleAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            await DoWorkAsync(workerContext);
        }
    }

}
