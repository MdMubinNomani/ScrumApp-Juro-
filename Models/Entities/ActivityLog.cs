using System.ComponentModel.DataAnnotations;
using System;

namespace ScrumApp__Juro_.Models.Entities
{
    public class ActivityLog
    {
        public int ActivityLogID { get; set; }

        [Required]
        public string Role { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Details { get; set; }
    }

}
