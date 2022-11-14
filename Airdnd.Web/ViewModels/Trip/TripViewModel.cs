using System;
using System.Collections.Generic;

namespace Airdnd.Web.ViewModels.Trip
{
    public class TripViewModel
    {
        public int Id { get; set; }
   
        public int orderId { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public int Status { get; set; }

        public string ListingName { get; set; }
        public string ListingDetail { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
    }
}
