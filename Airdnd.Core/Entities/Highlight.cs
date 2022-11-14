using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Highlight
    {
        public Highlight()
        {
            Listings = new HashSet<Listing>();
        }

        public int HighlightId { get; set; }
        public string HighlightName { get; set; }
        public int Sort { get; set; }
        public string IconPath { get; set; }
        public int? ListingId { get; set; }

        public virtual Listing Listing { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
    }
}
