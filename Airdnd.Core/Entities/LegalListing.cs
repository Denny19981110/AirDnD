using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class LegalListing
    {
        public int LlegalId { get; set; }
        public int LegalId { get; set; }
        public int ListingId { get; set; }

        public virtual Legal Legal { get; set; }
        public virtual Listing Listing { get; set; }
    }
}
