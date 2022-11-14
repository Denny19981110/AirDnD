using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Calendar
    {
        public int CalendarId { get; set; }
        public DateTime CalendarDate { get; set; }
        public decimal Price { get; set; }
        public int ListingId { get; set; }
        public bool Available { get; set; }
        public int? OrderId { get; set; }

        public virtual Listing Listing { get; set; }
        public virtual Order Order { get; set; }
    }
}
