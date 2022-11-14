using Airdnd.Web.Interfaces;
using Airdnd.Web.Models;
using Airdnd.Web.Services;
using Airdnd.Web.Services.Partial;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Partial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Airdnd.Web.Controllers
{
    public class HomeController :Controller
    {
        private readonly HomeService _productService;
        private readonly IProductService _mainProductService;
        private readonly FilterPartialService _filterService;
        private readonly IFilterPartialService _mainFilterService;
        private readonly ILogger<HomeController> _logger;
        public HomeController( HomeService productService, ILogger<HomeController> logger, FilterPartialService filterService, IProductService mainProductService, IFilterPartialService mainFilterService )
        {
            _productService = productService;
            _logger = logger;
            _filterService = filterService;
            _mainProductService = mainProductService;
            _mainFilterService = mainFilterService;
        }
        [HttpGet]
        public IActionResult Index(
            FilterPrice price,
            [FromQuery(Name = "Privacies[]")] IEnumerable<int> privacies,
             Rooms rooms,
            [FromQuery(Name = "Properties[]")] IEnumerable<int> properties,
            [FromQuery(Name = "Services[]")] IEnumerable<int> services,
            int page = 1
            )
        {
            //確認登入
            int userId = 0;
            if( User.Identity.IsAuthenticated )
            {
                userId = int.Parse(User.Identity.Name);
            };
            ViewBag.userId = userId;

            //filter

            var result = new HomeViewModel();
            if( price.CurrentMax == 0 )
            {
                var listings = _mainProductService.Get16Listings(userId, page);
                result.Listings = listings;
                result.FilterPartialVM = _mainFilterService.GetAllFilter();
                result.Properties = _mainProductService.GetHomeProperties();
                result.UserInfo = _productService.GetUserInfo(userId);
                result.PageCount = _productService.CountAllPages();
                result.CurrentPage = page;
            }
            else
            {
                var dto = new FilterPartialDto
                {
                    Price = price,
                    Privacies = privacies,
                    Rooms = rooms,
                    Properties = properties,
                    Services = services
                };
                result = _filterService.GetListingByFilter(dto, userId, page);
                ViewBag.filter = dto;
            }

            IQueryCollection urls = HttpContext.Request.Query;
            ViewBag.queries = urls;

            //社交連結測試
            SeoHelpDto seohelp = new SeoHelpDto
            {
                WebAddress = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value.ToString()}",
                Title = "AirDnD-2022年秋天你最優質的旅遊夥伴",
                Description = "尋找台灣各地的度假屋、小木屋、別墅、獨特房源和精彩體驗，由懷(燃)抱(燒)夢(肝)想(臟)的AirDnD團隊，與其房東和體驗達人為你實現。",
                Image = "https://kamiiliu.github.io/fake.png",
            };
            ViewData["seohelp"] = seohelp;

            if( userId != 0 )
            {
                //set wish
                result.Listings = _productService.SetIsWish(userId, result.Listings);
            }


            return View(result);
        }
        [HttpGet]
        [Route("prop/{propertyId}/{page=1}")]
        public IActionResult Index( int propertyId,
            FilterPrice price,
            [FromQuery(Name = "Privacies[]")] IEnumerable<int> privacies,
            Rooms rooms,
            [FromQuery(Name = "Properties[]")] IEnumerable<int> properties,
            [FromQuery(Name = "Services[]")] IEnumerable<int> services,
            [FromQuery] int page = 1 )
        {
            int userId = 0;
            if( User.Identity.IsAuthenticated )
            {
                userId = int.Parse(User.Identity.Name);
            };
            ViewBag.userId = userId;

            var listings = _productService.GetListingByProperty(propertyId, userId).ToList();
            var result = new HomeViewModel();
            if( price.CurrentMax == 0 )
            {
                result = new HomeViewModel
                {
                    Listings = listings,
                    Properties = _mainProductService.GetHomeProperties(),
                    UserInfo = _productService.GetUserInfo(userId),
                    FilterPartialVM = _mainFilterService.GetAllFilter(),
                };
                result.FilterPartialVM.Price = _filterService.SetPriceByResult(listings);
            }
            else
            {
                //filter

                var dto = new FilterPartialDto
                {
                    Price = price,
                    Privacies = privacies,
                    Rooms = rooms,
                    Properties = properties,
                    Services = services
                };

                result = _filterService.GetListingByFilter(dto, userId, page);
                result.Listings = _filterService.FilterListings(dto, listings, page);
                result.FilterPartialVM.Price = price;
                result.PageCount = _filterService.CountFilterPages(result);
                result.CurrentPage = page;
            }
            SeoHelpDto seohelp = new SeoHelpDto
            {
                WebAddress = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value.ToString()}",
                Title = "AirDnD-2022年秋天你最優質的旅遊夥伴",
                Description = "尋找台灣各地的度假屋、小木屋、別墅、獨特房源和精彩體驗，由懷(燃)抱(燒)夢(肝)想(臟)的AirDnD團隊，與其房東和體驗達人為你實現。",
                Image = "https://kamiiliu.github.io/fake.png",
            };
            ViewData["seohelp"] = seohelp;
            result.Listings = _productService.SetIsWish(userId, result.Listings);

            return View(result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
