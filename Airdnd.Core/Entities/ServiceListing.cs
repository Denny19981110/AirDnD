using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class ServiceListing
    {
        public int LserviceId { get; set; }
        public int ListingId { get; set; }
        public int ServiceId { get; set; }

        public virtual Listing Listing { get; set; }
        public virtual Service Service { get; set; }
    }
}
