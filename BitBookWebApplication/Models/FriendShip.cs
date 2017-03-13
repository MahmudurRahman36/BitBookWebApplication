using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookWebApplication.Models
{
    public class FriendShip
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int FriendshipID { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
}