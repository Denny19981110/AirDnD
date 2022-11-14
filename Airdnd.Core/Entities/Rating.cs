using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int? ListingId { get; set; }
        public int? UserId { get; set; }
        public double? RatingAvg { get; set; }
        public int? Clean { get; set; }
        public int? Precise { get; set; }
        public int? Communication { get; set; }
        public int? Location { get; set; }
        public int? CheckIn { get; set; }
        public int? CostPrice { get; set; }
        public string CommentContent { get; set; }
        public DateTime? CreatTime { get; set; }

        public virtual Listing Listing { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
