using System;

namespace Airdnd.Web.ViewModels
{
    public class SellerIndexViewModel
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string ListingName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
