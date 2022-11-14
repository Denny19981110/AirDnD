using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.ViewModels
{
    public class RoomSourceViewModel
    {
        public IEnumerable<Page1> PropertyGroup { get; set; }
        public IEnumerable<Page2> PropertyType { get; set; }
        public IEnumerable<Page3> PrivacyType { get; set; }
        public Page4 Map { get; set; }
        public Page5 ListingQuantity { get; set; }
        public IEnumerable<Page6> Service { get; set; }
        public IEnumerable<Page7> RoomImage { get; set; }
        public Page8 RoomName { get; set; }
        public IEnumerable<Page9> HighLight { get; set; }
        public Page10 Description { get; set; }
        public Page11 RoomRate { get; set; }
        public IEnumerable<Page12> Legal { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        public class Page1
        {
            public int PropertyGroupId { get; set; }
            public string PropertyGroupName { get; set; }
            public string PropertyGroupImgUrl { get; set; }
        }
        public  class Page2
        {
            public int PropertyTypeId { get; set; }
            public int PropertyGroupId { get; set; }
            public string PropertyTitle { get; set; }
            public string PropertyContent { get; set; }
        }
        public class Page3
        {
            public int PrivacyTypeId { get; set; }
            public string PrivacyTypeName { get; set; }
        } 
        public class Page4
        {
            public string Address { get; set; }

        }
        public class Page5
        {
            public int People { get; set; }
            public int Bed { get; set; }
            public int Bedroom { get; set; }
            public int Bathroom { get; set; }
            public bool InsideBathroom { get; set; }
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
        public class Page7
        {
            public string RoomImageUrl { get; set; }
        }
        public class Page8
        {
            public string RoomName { get; set; }
        }
        public class Page9
        {
            public int HighLightId { get; set; }
            public string HighLightName { get; set; }
            public string HighLightIconPath { get; set; }
        }
        public class Page10
        {
            public string Description { get; set; }
        }
        public class Page11
        {
            public decimal Price { get; set; }
            public bool Promotion { get; set; }
        }
        public class Page12
        {
            public int LegalId { get; set; }
            public string LegalName { get; set; }
        }
    }
        
}

