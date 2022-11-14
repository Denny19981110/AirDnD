using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Promo
    {
        public int PromoId { get; set; }
        public decimal Discount { get; set; }
        public string PromoName { get; set; }
        public int ListingId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? EditTime { get; set; }

        public virtual Listing Listing { get; set; }
    }
}
