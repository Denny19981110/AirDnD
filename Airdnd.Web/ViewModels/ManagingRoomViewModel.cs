using Airdnd.Core.enums;
using System;

namespace Airdnd.Web.ViewModels
{
    public class ManagingRoomViewModel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int HostId { get; set; }
        public StatusType statusType { get; set; }
        public int Bed { get; set; }
        public int BedRoom { get; set; }
        public int BathRoom { get; set; }
        public DateTime Createtime { get; set; }
        public string Roomphoto { get; set; }
        public string Address { get; set; }
        public string Country => Address.Split("-")[0];
        public string City => Address.Split("-")[1];
        public string Url { get; set; }
    }
}
