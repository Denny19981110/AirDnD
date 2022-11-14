using System.Collections;
using System.Collections.Generic;

namespace Airdnd.Web.Models.DtoModels.RoomSource
{
    public class RoomSourceDto
    {
        public Page1 PropertyGroup { get; set; }
        public Page2 PropertyType { get; set; }
        public Page3 PrivacyType { get; set; }
        public Page6 Service { get; set; }
        public Page9 HighLight { get; set; }
        public Page12 Legal { get; set; }
        public string UserName { get; set; }
        public class Page1
        {
            public int PropertyGroupId { get; set; }
            public string PropertyGroupName { get; set; }
            public string PropertyGroupImgUrl { get; set; }
        }
        public class Page2
        {
            public int PropertyTypeId { get; set; }
            public string PropertyTitle { get; set; }
            public string PropertyContent { get; set; }
            public int PropertyGroupId { get; set; }
        }
        public class Page3
        {
            public int PrivacyTypeId { get; set; }
            public string PrivacyTypeName { get; set; }
        }
        public class Page6
        {
            public int ServiceTypeId { get; set; }
            public string ServiceTypeName { get; set; }
            public IEnumerable<ServiceItem> ServiceItems { get; set; }
            public class ServiceItem
            {
                public int ServiceTypeId { get; set; }
                public int ServiceId { get; set; }
                public string Service { get; set; }
                public string ServiceIconPath { get; set; }
                public int Sort { get; set; }
            }
        }
        public class Page9
        {
            public int HighLightId { get; set; }
            public string HighLightName { get; set; }
            public string HighLightIconPath { get; set; }
        }
        public class Page12
        {
            public int LegalId { get; set; }
            public string LegalName { get; set; }
        }

    }
}
