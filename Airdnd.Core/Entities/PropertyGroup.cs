using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class PropertyGroup
    {
        public PropertyGroup()
        {
            PropertyTypes = new HashSet<PropertyType>();
        }

        public int PropertyGroupId { get; set; }
        public string PropertyGroupName { get; set; }
        public string IconPath { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<PropertyType> PropertyTypes { get; set; }
    }
}
