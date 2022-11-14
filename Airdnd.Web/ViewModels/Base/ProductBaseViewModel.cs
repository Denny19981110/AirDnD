using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace Airdnd.Web.ViewModels.Base
{
    public class ProductBaseViewModel
    {
        [DisplayName("房源ID")]
        public int Id { get; set; }
        [DisplayName("房源名稱")]
        public string ListingName { get; set; }
        public List<string> ImgPath { get; set; }

        public Location Location { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
        public string PriceGet
        {
            get { return Price.ToString("##,###"); }
        }
        public string Rating { get; set; }
        public bool IsWish { get; set; }
    }
    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
