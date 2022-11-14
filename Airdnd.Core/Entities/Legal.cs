using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Legal
    {
        public Legal()
        {
            LegalListings = new HashSet<LegalListing>();
        }

        public int LegalId { get; set; }
        public string LegalName { get; set; }
        public int Sort { get; set; }

        public virtual ICollection<LegalListing> LegalListings { get; set; }
    }
}
