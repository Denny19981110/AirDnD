using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class WishList
    {
        public WishList()
        {
            WishListDetails = new HashSet<WishListDetail>();
        }

        public int WishlistId { get; set; }
        public int UserAccountId { get; set; }
        public string WishGroupName { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual ICollection<WishListDetail> WishListDetails { get; set; }
    }
}
