using System;

namespace Airdnd.Web.ViewModels.House
{
    public class OrderDateVM
    {
        public int houseID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime lastDate { get; set; }
        public decimal total { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
    }
}
