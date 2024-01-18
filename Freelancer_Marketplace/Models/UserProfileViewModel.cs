using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelancer_Marketplace.Models
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsActive { get; set; }

        // Additional properties from the user_profiles table
        public int ProfileId { get; set; }
        public string Bio { get; set; }
        public string Portfolio { get; set; }
        public string FullName { get; set; }
        public string Location { get; set; }
        public string Skills { get; set; }
        public string ContactInformation { get; set; }
        public string SocialMediaLinks { get; set; }
        [Display(Name = "Profile Picture")]
        public HttpPostedFileBase UploadedImage { get; set; }
    }
}