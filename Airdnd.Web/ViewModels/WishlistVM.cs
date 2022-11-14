using Airdnd.Web.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace Airdnd.Web.ViewModels
{
    public class WishlistVM
    {
        public int WishlistID { get; set; }
        public string WishlistName { get; set; }
        public List<WishlistDetailVM> WishlistDetail { get; set; }
        public int UserAcountId { get; set; }
    }
    public class WishlistDetailVM :ProductBaseViewModel
    { 
        public string PropertyTitle { get; set; }
        public int Beds { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        /// <summary>
        /// 入住人數
        /// </summary>
        public int GuestExpected { get; set; }
        public DateTime CreatTime { get; set; }
    }
}
