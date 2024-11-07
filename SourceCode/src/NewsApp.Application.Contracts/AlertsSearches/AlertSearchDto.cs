using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using NewsApp.Notifications;
using NewsApp.Searches;
using NewsApp.Users;

namespace NewsApp.AlertsSearches
{
    public class AlertSearchDto : EntityDto<int>
    {
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }

        // relaciones
        public Guid SearchOfAlertId { get; set; }
        public SearchDto Search { get; set; }
        public UserDto User { get; set; }
        public ICollection<NotificationDto> Notifications { get; set; }
    }
}
