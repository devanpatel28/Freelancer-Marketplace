using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelancer_Marketplace.Models
{
    public class UploadedDataViewModel
    {
        public int FilesId { get; set; }
        public int ProjectId { get; set; }
        public int FreelancerId { get; set; }
        public string FilePath { get; set; }
        public bool IsVerified { get; set; }
        public DateTime UploadTime { get; set; }
        public string ForMessage { get; set; }

        public int TakenProjectsId { get; set; }
        public int States { get; set; }
    }
}