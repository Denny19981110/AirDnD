using System;

namespace Airdnd.Web.ViewModels
{
    public class TransactionViewModel
    {
        public int ListingId { get; set; }
        public string Listingname { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public decimal price { get; set; }
        public string PhotoUrl { get; set; }
        public int Status { get; set; }
    }


}
