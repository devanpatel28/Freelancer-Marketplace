using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelancer_Marketplace.Models
{
    public class MyBidsViewModel
    {
        // Properties from the projects table
        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string BrandName { get; set; }
        public string ProjectDescription { get; set; }
        public decimal Budget { get; set; }
        public DateTime Deadline { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime DateCreated { get; set; }

        // Properties from the bids table
        public int BidId { get; set; }
        public int FreelancerId { get; set; }
        public decimal BidAmount { get; set; }
        public string ProposalText { get; set; }
        public bool IsApproved { get; set; }
        public DateTime BidTime { get; set; }
    }

}