using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelancer_Marketplace.Models
{
    public class BidTheProjectViewModel
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int Freelancer_id { get; set; }
        [Required]
        public int Bid_amount { get; set; }
        [Required]
        public bool is_approved { get; set; }
        [Required]
        public string Proposal_text { get; set; }
    }
}