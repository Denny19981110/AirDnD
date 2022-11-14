using System;

namespace Airdnd.Web.Models.DtoModels
{
    public class OrderDto
    {
        public string Roomname { get; set; }
        public string Startdate { get; set; }
        public string Enddate { get; set; }
        public string Address { get; set; }
        public decimal Totalprice { get; set; }
        public int Customer { get; set; }
        public string Email { get; set; }
        public string RoomPhoto { get; set; }
        public int OrderId { get; set; }
        public string? PayId { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
