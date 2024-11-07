using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Notifications;
using NewsApp.Searches;
using Volo.Abp.Application.Services;

namespace NewsApp.AlertsSearches
{
    public interface IAlertSearchAppService : IApplicationService
    {
        Task<AlertSearchDto> CreateAlertAsync(int searchId);
        Task<ICollection<NotificationDto>> NotifyUserOfNewResultsAsync();    
    }
}
