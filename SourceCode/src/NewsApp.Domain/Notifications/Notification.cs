using System;
using NewsApp.AlertsSearches;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace NewsApp.Notifications
{
    public class Notification : Entity<int>
    {
        public bool Active { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public AlertSearch Alert { get; set; }
        public int AlertId { get; set; }
        public IdentityUser User { get; set; }
    }
}
