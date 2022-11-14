using System;

namespace Airdnd.Web.Models.DtoModels
{
    public class OrderJsDto
    {
        public int RoomId { get; set; }
        public int Adult { get; set; }
        public int Children { get; set; }
        public int Infants { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }

    }
}
