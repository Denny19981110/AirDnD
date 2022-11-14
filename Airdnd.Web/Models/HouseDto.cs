using Airdnd.Core.enums;
using System;
using System.Collections.Generic;

namespace Airdnd.Web.Models
{
    public class HouseDto
    {
        public int HouseID { get; set; }
        public HostDto Host { get; set; }

        public int star { get; set; }
        public string HouseName { get; set; }
       
       
        public double Lng { get; set; }
        public double Lat { get; set; }
        public string Description { get; set; }
        public string Property { get; set; }
        public string Category { get; set; }
        public int Expected { get; set; }
        public StatusType Status { get; set; }
        public int Bed { get; set; }
        public int BedRoom { get; set; }
        public int BathRoom { get; set; }
        public int Toilet { get; set; }
        public bool IndieBathroom { get; set; }
        public string Highlight { get; set; }
        public List<ListIMGDto> HousePic { get; set; }
        public decimal DefaultPrice { get; set; }
        public string Address { get; set; }
        public string Country => Address.Split("-")[0];
        public string City => Address.Split("-")[1];

    }
    
}
