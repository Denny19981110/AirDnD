using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class OrderDate
    {
        public int OrderDateId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderedDate { get; set; }
        public bool Available { get; set; }

        public virtual Order Order { get; set; }
    }
}
