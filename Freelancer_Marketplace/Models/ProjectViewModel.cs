using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelancer_Marketplace.Models
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string BrandName { get; set; }
        public string ProjectDescription { get; set; }
        public decimal? Budget { get; set; }
        public DateTime? Deadline { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime? DateCreated { get; set; }
        public string ClientUsername { get; set; }
        public string FormattedStartDate { get; set; }
        public string FormattedDeadline { get; set; }
    }

}