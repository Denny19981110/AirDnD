using System;

namespace Airdnd.Web.ViewModels
{
    public class CalendarViewModel
    {
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public DateTime CalendarDate { get; set; }
        public int ListingId { get; set; }
        public string ListingName { get; set; }
        public int? OrderId { get; set; }
       
    }
}
