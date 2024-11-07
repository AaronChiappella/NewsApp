using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NewsApp.Notifications
{
    public interface INotificationAppService : IApplicationService
    {
        Task<NotificationDto> CreateNotificationAsync(CreateUpdateNotificationDto input);

        Task<ICollection<NotificationDto>> GetNotificationsAsync();
    }
}
