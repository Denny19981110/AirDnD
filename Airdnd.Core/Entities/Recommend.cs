using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Recommend
    {
        public Recommend()
        {
            RecommendListings = new HashSet<RecommendListing>();
        }

        public int RecommendId { get; set; }
        public string RecommendName { get; set; }
        public string RecommendContent { get; set; }

        public virtual ICollection<RecommendListing> RecommendListings { get; set; }
    }
}
