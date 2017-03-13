using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookWebApplication.Models
{
    public class AllAboutPost
    {
        public int Id { get; set; }
        public string PostDetail { get; set; }
        public int NoOfLike { get; set; }
        public DateTime DateTime { get; set; }
        public int SignUpID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public byte[] ProfilePhotoInByte { get; set; }
        public string ProfilePhotoInString { get; set; }
        public byte[] PostPhotoInByte { get; set; }
        public string PostPhotoInString { get; set; }
    }
}