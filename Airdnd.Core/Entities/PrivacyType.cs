using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class PrivacyType
    {
        public PrivacyType()
        {
            Listings = new HashSet<Listing>();
        }

        public int PrivacyTypeId { get; set; }
        public string PrivacyTypeName { get; set; }
        public string PrivacyTypeContent { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }
    }
}
