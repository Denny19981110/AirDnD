using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class RecommendListing
    {
        public int LrecommendId { get; set; }
        public int RecommendId { get; set; }
        public int ListingId { get; set; }

        public virtual Listing Listing { get; set; }
        public virtual Recommend Recommend { get; set; }
    }
}
