using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookWebApplication.Models
{
    public class CommentDetail
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int PersonID { get; set; }
        public string PersonName { get; set; }
        public string Comment { get; set; }
        public int TypeNo { get; set; }
    }
}