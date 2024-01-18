using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelancer_Marketplace.Models
{
    public class ShowBidOfMyProjectViewModel
    {
        public int BidId { get; set; }
        public int ProjectId { get; set; }
        public int FreelancerId { get; set; }
        public decimal BidAmount { get; set; }
        public string ProposalText { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? BidTime { get; set; }  // Nullable DateTime

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime? RegistrationDate { get; set; }  // Nullable DateTime
        public DateTime? LastLoginDate { get; set; }  // Nullable DateTime
        public bool IsActive { get; set; }
    }
}