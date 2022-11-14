using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class ListingImage
    {
        public int ListingImageId { get; set; }
        public string ListingImagePath { get; set; }
        public int ListingId { get; set; }

        public virtual Listing Listing { get; set; }
    }
}
