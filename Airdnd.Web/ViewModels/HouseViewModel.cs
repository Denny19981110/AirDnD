using Airdnd.Web.Models;
using System.Collections.Generic;

namespace Airdnd.Web.ViewModels
{
    public class HouseViewModel
    {
        public int count { get; set; }
        public bool IsComment { get; set; }
        public RankDto RankSix { get; set; }
        public double RatingAvg { get; set; }
        public List<RankPersonDto> RankPerson { get; set; }

    }
}
