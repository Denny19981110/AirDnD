using System;

namespace Airdnd.Web.ViewModels
{
    public class BookingViewModel
    {
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string Checkintime { get; set; }
        public int Customer { get; set; }
        public int ListingId { get; set; }

        public int CustomerId { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Infants { get; set; }
        public string Roomname { get; set; }
        public string RoomPhoto { get; set; }
        public string Roomdescription { get; set; }
        public double? Rating { get; set; }
        public int Comment { get; set; }
        public int Staying { get; set; }
        public decimal Stayingprice { get; set; }
        public decimal Cleaningfee { get; set; }
        public decimal Servicecharge { get; set; }
        public decimal Totalprice { get; set; }
        public int? OrderId { get; set; }
    }
}
