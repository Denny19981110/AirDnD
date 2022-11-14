using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public int OrderId { get; set; }
        public int Rating { get; set; }
        public DateTime CreatTime { get; set; }
        public int? Clean { get; set; }
        public int? Precise { get; set; }
        public int? Communication { get; set; }
        public int? Location { get; set; }
        public int? CheckIn { get; set; }
        public int? CostPrice { get; set; }
        public int? HostId { get; set; }

        public virtual UserAccount Host { get; set; }
        public virtual Order Order { get; set; }
    }
}
