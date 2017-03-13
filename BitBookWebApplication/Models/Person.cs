using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookWebApplication.Models
{
    public class Person
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email Address not valid")]
        //[RegularExpression(@"(\W|^)[\w.+\-]*@gmail\.com(\W|$)", ErrorMessage = "Email Address not valid gmail address")]
        public string Email { get; set; }
        [Required]
        [Display(Name="Confirm Email")]
        [Compare("Email")]
        public string ConfirmEmail { get; set; }
        public string MobileNo { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        public string ProfileID { get; set; }
    }
}