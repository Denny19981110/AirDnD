using System.Collections.Generic;

namespace Airdnd.Web.Models
{
    public class CService
    {
        public int count { get; set; }
        public List<LService> ServiceList { get; set; }
    }
    public class LService
    {
        public int ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; }
        public List<IService> ServiceIcon { get; set; }
    }
    public class IService
    {
        public int ServiceTypeID { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Sort { get; set; }
        public string IconPath { get; set; }
    }
}
