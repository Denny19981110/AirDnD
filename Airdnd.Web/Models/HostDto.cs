using System.Collections.Generic;

namespace Airdnd.Web.Models
{
    public class HostDto
    {
        public int HostID { get; set; }
        public string HostName { get; set; }
        public string AvatarUrl { get; set; }
        public string HostAboutMe { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public List<ListDto> House { get; set; }
    }
    public class ListDto
    {
        public int HostID { get; set; }
        public double? star { get; set; }
        public string ListingName { get; set; }
        public decimal DefaultPrice { get; set; }
        public string Address { get; set; }
        public string Country => Address.Split("-")[0];
        public string City => Address.Split("-")[1];
        public List<ListIMGDto> ListIMG { get; set; }
    }
    public class ListIMGDto
    {
        public string AvatarUrl { get; set; }
    }
}
