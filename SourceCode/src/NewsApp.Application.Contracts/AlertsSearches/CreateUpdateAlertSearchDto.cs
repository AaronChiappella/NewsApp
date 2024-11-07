using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using NewsApp.Notifications;
using NewsApp.Searches;

namespace NewsApp.AlertsSearches
{
    public class CreateUpdateAlertSearchDto
    {
        [Required]
        public SearchDto Search { get; set; }
        [Required]
        public Guid SearchOfAlertId { get; set; }

        // public ICollection<NotificationDto> Notifications { get; set; }
    }
}
