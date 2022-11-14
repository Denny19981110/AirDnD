using Airdnd.Core.enums;

namespace Airdnd.Admin.Models.DtoModels
{
    public class ChartDto
    {
        public dynamic TotalMembers { get; set; }
        public dynamic ThisMonthOrders { get; set; }
        public dynamic ThisYearOrders { get; set; }
        public dynamic ThisMonthRevenue { get; set; }
        public dynamic ThisYearRevenue { get; set; }
        public dynamic[] EveryMonthOrders { get; set; }
        public dynamic[] EveryMonthRevenue { get; set; }
        public dynamic[] EverySeasonMembers { get; set; }
        public dynamic[] EverySeasonListing { get; set; }
        public dynamic[] MembersGender { get; set; }
        
    }
}
