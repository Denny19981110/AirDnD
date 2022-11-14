using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class WishListDetail
    {
        public int WishlistDetailId { get; set; }
        public int WishlistId { get; set; }
        public int ListingId { get; set; }
        public DateTime CreatTime { get; set; }

        public virtual Listing Listing { get; set; }
        public virtual WishList Wishlist { get; set; }
    }
}
