using System;

namespace Airdnd.Web.Models.DtoModels
{
    public class UnpayOrderDto
    {
        public string Roomphoto { get; set; }
        public string Roomname { get; set; }
        public string Roomdescription { get; set; }
        public decimal Cleaningfee { get; set; }
        public decimal Servicecharge { get; set; }
        public int Customer { get; set; }
        public int Staying { get; set; }
        public int ListingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Children { get; set; }
        public int Infants { get; set; }
        public int Adults { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
