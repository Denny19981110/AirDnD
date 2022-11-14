using System;

namespace Airdnd.Web.Models
{
    public class RankAverDto
    {
        public int ListingId { get; set; }
        public double RatingAvg => Math.Round(RatingAvg, 2);
    }
    public class RankDto
    {
        public int ListingId { get; set; }
        public double Clean { get; set; }
        public double Precise { get; set; }
        public double Communication { get; set; }
        public double Location { get; set; }
        public double CheckIn { get; set; }
        public double CostPrice{ get; set; }
}
    public class RankPersonDto
    {
        public int userId { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string CommentContent { get; set; }
        public string country { get; set; }
        public bool flag { get; set; }  
        public string time { get; set; }
        public DateTime timeflag { get; set; }
    }
}
