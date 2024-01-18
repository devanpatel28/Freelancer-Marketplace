using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelancer_Marketplace.Models
{
    public class ProjectBidViewModel
    {
        public List<ProjectViewModel> Projects { get; set; }
        public BidTheProjectViewModel BidViewModel { get; set; }
    }
}