using Airdnd.Core.enums;
using System;

namespace Airdnd.Admin.Models
{
    public class ListingDto
    {
        public int ListingId { get; set; }
        public string ListingName { get; set; }
        public string PropertyName { get; set; }
        public string Price { get; set; }
        public string Host { get; set; }
        public string Region { get; set; }
     
        public int BedRooms { get; set; }
        public int Beds { get; set; }
        public int BathRooms { get; set; }
        public string CreateTime { get; set; }
        public StatusType Status { get; set; }
    }
    public class ListingCondition {
        public int ListingId { get; set; }
        public int Status { get; set; }
    }
    public class ListingViewModel
    {
        public int Id { get; set; }
        public string ListingName { get; set; }
        public string PropertyName { get; set; }
        public string Price { get; set; }
        public string Host { get; set; }
        public string Region { get; set; }
        public int BedRooms { get; set; }
        public int Beds { get; set; }
        public int BathRooms { get; set; }
        public string CreateTime { get; set; }
        public StatusType Status { get; set; }
    }
}
