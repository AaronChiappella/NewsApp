using System;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Notifications
{
    public class CreateUpdateNotificationDto
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Required]
        public int AlertId { get; set; }
        }
    }