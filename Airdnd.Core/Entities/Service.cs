using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Service
    {
        public Service()
        {
            ServiceListings = new HashSet<ServiceListing>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int ServiceTypeId { get; set; }
        public int Sort { get; set; }
        public string IconPath { get; set; }

        public virtual ServiceType ServiceType { get; set; }
        public virtual ICollection<ServiceListing> ServiceListings { get; set; }
    }
}
