using System;

namespace Airdnd.Web.Models.DtoModels
{
    public class CalendarDto
    {
        public int CalendarId { get; set; }
        public decimal Price { get; set; }
        public decimal DefultPrice { get; set; }
        public bool Available { get; set; }
        public DateTime CalendarDate { get; set; }
        public int ListingId { get; set; }
        public CalendarSelect ListingInfo { get; set; }
        public int? OrderId { get; set; }
        public class CalendarSelect
        {
            public string ListingName { get; set; }
            public int ListingId { get; set; }
        }
    }
}
