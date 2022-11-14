using System.Collections.Generic;

namespace Airdnd.Web.ViewModels
{
    public class RoomInfoViewModel
    {

        public int Id { get; set; }
        public string Address { get; set; }
        public int People { get; set; }
        public int Bed { get; set; }
        public int BedRoom { get; set; }
        public int BathRoom { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        //房源種類
        public string PropertyGroup { get; set; }
        //住宿類型
        public string PropertyType { get; set; }
        //房源類型
        public string PrivacyType { get; set; }
        public IEnumerable<string> Photos { get; set; }

        public string Message { get; set; }
        public bool IsSuccess { get; set; }

    }
}
