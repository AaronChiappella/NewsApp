using System;
using System.Collections.Generic;
using NewsApp.Notifications;
using NewsApp.Searches;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace NewsApp.AlertsSearches
{
    public class AlertSearch : Entity<int>
    {
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }

        // relaciones
        public Search Search { get; set; }
        public int AlertOfSearchId { get; set; }
        public required IdentityUser User { get; set; }
        public ICollection<Notification> Notifications { get; set; }

        public AlertSearch()
        {
            Active = true;
            CreatedDate = DateTime.Now;
        }

    }
}
