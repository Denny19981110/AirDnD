using System;
using System.Collections.Generic;
using System.ComponentModel;
using Airdnd.Web.ViewModels.Base;
using Airdnd.Web.ViewModels.Partial;

namespace Airdnd.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ProductDto> Listings { get; set; }
        public IEnumerable<HomePropertyDto> Properties { get; set; }
        public UserInfoDto UserInfo { get; set; }
        public FilterPartialVM FilterPartialVM { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
    public class UserInfoDto
    {
        public int UserId { get; set; }
        public Location UserLocation { get; set; }
    }
    public class ProductDto :ProductBaseViewModel
    {
        public string PropertyTitle { get; set; }
        [DisplayName("不可入住日期")]
        public IEnumerable<DateTime> NotAvailableDate { get; set; }
        public int PropertyId { get; set; }
        public IEnumerable<int> ServiceId { get; set; }
        public int PrivacyId { get; set; } 
        public Rooms Rooms { get; set; }
        public int Expected { get; set; }
        public string County { get; set; }
    }
    public class HomePropertyDto
    {
        public int Id { get; set; }
        public string PropertyTitle { get; set; }
        public string IconPath { get; set; }
    }

}
