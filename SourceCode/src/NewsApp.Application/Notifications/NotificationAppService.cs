using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NewsApp.AlertsSearches;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace NewsApp.Notifications
{
    public class NotificationAppService : NewsAppAppService, INotificationAppService
    {
        private readonly IRepository<Notification, int> _notificationRepository;
        private readonly IRepository<AlertSearch, int> _alertSearchRepository;
        private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;


        public NotificationAppService(IRepository<Notification, int> notificationRepository, IRepository<AlertSearch, int> alertSearchRepository, UserManager<Volo.Abp.Identity.IdentityUser> userManager)
        {
            _notificationRepository = notificationRepository;
            _alertSearchRepository = alertSearchRepository;
            _userManager = userManager;
        }

        public async Task<ICollection<NotificationDto>> GetNotificationsAsync()
        {
            var userGuid = CurrentUser.Id.GetValueOrDefault();

            var identityUser = await _userManager.FindByIdAsync(userGuid.ToString());

            var notifications = await _notificationRepository.GetListAsync(n => n.User == identityUser);

            return ObjectMapper.Map<ICollection<Notification>, ICollection<NotificationDto>>(notifications);
        }


        public async Task<NotificationDto> CreateNotificationAsync(CreateUpdateNotificationDto input)
        {
            var userGuid = CurrentUser.Id.GetValueOrDefault();

            var identityUser = await _userManager.FindByIdAsync(userGuid.ToString());

            var alert = await _alertSearchRepository.FindAsync(input.AlertId);

            var notification = new Notification
            {
                Title = $"Se han encontrado nuevos resultados para la búsqueda '{input.Title}'",
                Active = true,
                DateTime = input.DateTime,
                Alert = alert,
                AlertId = input.AlertId,
                User = identityUser
            };

            notification = await _notificationRepository.InsertAsync(notification, autoSave: true);

            alert.Notifications.Add(notification);

            await _alertSearchRepository.UpdateAsync(alert);

            return ObjectMapper.Map<Notification, NotificationDto>(notification);
        }
    }
}
