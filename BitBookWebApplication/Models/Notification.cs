using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookWebApplication.Models
{
    public class Notification
    {
        public int Type { get; set; }
        public int TypeId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string NotificationString { get; set; }
        public DateTime DateTime { get; set; }
    }
}