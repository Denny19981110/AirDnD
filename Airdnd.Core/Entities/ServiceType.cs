using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class ServiceType
    {
        public ServiceType()
        {
            Services = new HashSet<Service>();
        }

        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
