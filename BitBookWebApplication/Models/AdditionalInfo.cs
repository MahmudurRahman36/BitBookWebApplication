﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookWebApplication.Models
{
    public class AdditionalInfo
    {
        public int ID { get; set; }
        public int SignupID { get; set; }
        public int ProfilePhotoId { get; set; }
        public HttpPostedFileBase ProfilePhoto { get; set; }
        public byte[] PhotoInByte { get; set; }
        public int CoverPhotoId { get; set; }
        public HttpPostedFileBase CoverPhoto { get; set; }
        public byte[] CoverPhotoInByte { get; set; }
        public string AboutMe { get; set; }
        public string Religion { get; set; }
        public string MobileNumber { get; set; }
        public int EducationID { get; set; }
        public string EdLevel { get; set; }
        public string EdName { get; set; }
        public string Edyear { get; set; }
        public int JobID { get; set; }
        public string JobName { get; set; }
        public string JobPosition { get; set; }
        public string JobBegin { get; set; }
        public string JobEnd { get; set; }
        public string Passowrd { get; set; }
    }
}