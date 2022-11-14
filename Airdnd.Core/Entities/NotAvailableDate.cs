using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class NotAvailableDate
    {
        public int NotAvailableId { get; set; }
        public int ListingId { get; set; }
        public DateTime Date { get; set; }
        public int? OrderDateId { get; set; }
        public int? CalendarId { get; set; }
        public bool Available { get; set; }

        public virtual Calendar Calendar { get; set; }
        public virtual OrderDate OrderDate { get; set; }
    }
}
