using System;
using System.Collections.Generic;
using System.ComponentModel;
using Airdnd.Web.ViewModels.Base;
using Airdnd.Web.ViewModels.Partial;

namespace Airdnd.Web.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<ProductDto> ResultListings { get; set; }
        public FilterPartialVM Filter { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
    public class SearchProductDto :ProductBaseViewModel
    {

        public string County { get; set; }
        //縣市
        public string PropertyTitle { get; set; }
        public int Beds { get; set; }
        public IEnumerable<DateTime> NotAviableDate { get; set; }
        public int Expected { get; set; }
    }
}
