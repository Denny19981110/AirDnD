using Airdnd.Web.ViewModels.Base;

namespace Airdnd.Web.Models
{
    public class MapDto
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public string ListingName { get; set; }
        public decimal Price { get; set; }
        public string PriceGet
        {
            get
            {
                return Price.ToString("##,###");
            }
        }
    }
}
