using Airdnd.Web.Controllers;
using Airdnd.Web.Services.Partial;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels.Partial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Airdnd.Web.ViewModels;

namespace Airdnd.Web.WebApi
{

    [ApiController]
    public class FilterController :ControllerBase
    {
        private readonly HomeService _productService;
        private readonly FilterPartialService _filterService;
        private readonly ILogger<HomeController> _logger;
        public FilterController( HomeService productService, ILogger<HomeController> logger, FilterPartialService filterService )
        {
            _productService = productService;
            _logger = logger;
            _filterService = filterService;
        }
        //[Route("api/filter")]
        //[HttpPost]
        //public IActionResult FilterSet( [FromBody] FilterPartialDto dto,int page )
        //{
        //    int userId = 0;
        //    if( User.Identity.IsAuthenticated )
        //    {
        //        userId = int.Parse(User.Identity.Name);
        //    };
        //    var listingList = _productService.Get16Listings(userId,page);
        //    var result = _filterService.FiltListings(dto, listingList);
       

        //    return RedirectToAction("Index", "Home", result);
        //}
    }
}
