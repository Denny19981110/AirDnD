using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class PropertyType
    {
        public PropertyType()
        {
            Listings = new HashSet<Listing>();
        }

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyContent { get; set; }
        public int PropertyGroupId { get; set; }
        public string IconPath { get; set; }

        public virtual PropertyGroup PropertyGroup { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
    }
}
