using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRAActivityLibrary
{
    
        public class VendorRequestInfo
        {
            public Guid Id { get; set; }

            public string RequesterId { get; set; }

            public DateTime CreationDate { get; set; }

            public string FacilityId { get; set; }

            public string Approvers { get; set; }

            public Guid WorkflowInstanceId { get; set; }

            public bool IsCompleted { get; set; }

            public bool IsSuccess { get; set; }

            public bool IsCancelled { get; set; }
        }
   
}

