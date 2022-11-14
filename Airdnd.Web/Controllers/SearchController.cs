using Airdnd.Web.Interfaces;
using Airdnd.Web.Models;
using Airdnd.Web.Services;
using Airdnd.Web.Services.Partial;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Base;
using Airdnd.Web.ViewModels.Partial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Airdnd.Web.Controllers
{
    public class SearchController :Controller
    {

        private readonly ISearchService _searchService;
        private readonly FilterPartialService _filterService;

        public SearchController( ISearchService searchService, FilterPartialService filterService )
        {
            _searchService = searchService;
            _filterService = filterService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("search/{location}/{checkIn}_{checkOut}/{guest}")]
        [HttpGet]
        public IActionResult Search( string location, string checkIn, string checkOut, string guest,
             FilterPrice price,
            [FromQuery(Name = "Privacies[]")] IEnumerable<int> privacies,
             Rooms rooms,
            [FromQuery(Name = "Properties[]")] IEnumerable<int> properties,
            [FromQuery(Name = "Services[]")] IEnumerable<int> services,
             int page = 1 )
        {
            if( location != "Any" )
                ViewBag.searchKeyword = location;
            else
                ViewBag.searchKeyword = "任何地點";
            //SeoHelp
            SeoHelpDto seoHelp = new SeoHelpDto
            {
                WebAddress = "https://testairdnd.azurewebsites.net/",
                Title = "AirDnD-2022年秋天你最優質的旅遊夥伴",
                Description = "尋找台灣各地的度假屋、小木屋、別墅、獨特房源和精彩體驗，由懷(燃)抱(燒)夢(肝)想(臟)的AirDnD團隊，與其房東和體驗達人為你實現。",
                Image = "https://kamiiliu.github.io/fake.png",
            };
            ViewData["seohelp"] = seoHelp;

            //Search Result
            int userId = 0;
            if( User.Identity.IsAuthenticated )
            {
                userId = int.Parse(User.Identity.Name);
            }
            ViewBag.userId = userId;

            var adult = int.Parse(guest.Split('-')[0]);
            var child = int.Parse(guest.Split('-')[1]);

            var allListing = _searchService.GetListingByConditions(location, userId, checkIn, checkOut, adult, child).ToList();

            //Filter
            IEnumerable<ProductDto> filtListings = new List<ProductDto>();

            var filter = _filterService.GetAllFilter();

            var dto = new FilterPartialDto
            {
                Price = price,
                Privacies = privacies,
                Rooms = rooms,
                Properties = properties,
                Services = services
            };
            var prices = new FilterPrice();

            if( price.CurrentMax > 0 )
            {
                filtListings = _filterService.FilterListings(dto, allListing, page);
                filter.Price = _filterService.SetPriceByResult(allListing);
                filter.Price.CurrentMax = price.CurrentMax;
                filter.Price.CurrentMin = price.CurrentMin;
                allListing = filtListings.ToList();
            }
            else
            {
                filter.Price = _filterService.SetPriceByResult(allListing);
            }
            ViewBag.resultCount = allListing.Count;
            SearchViewModel result = new()
            {
                ResultListings = _searchService.Take16Listings(page, allListing),
                Filter = filter,
                CurrentPage = page,
                PageCount = _searchService.CountAllPages(allListing)
            };
            IQueryCollection urls = HttpContext.Request.Query;
            ViewBag.queries = urls;
            var searchData = new
            {
                location = location,
                guest = new
                {
                    adult = adult,
                    child = child,
                    baby = int.Parse(guest.Split('-')[2])
                },
                date1 = checkIn,
                date2 = checkOut
            };
            ViewBag.SearchData = searchData;

            return View("Index", result);
        }
    }
}
