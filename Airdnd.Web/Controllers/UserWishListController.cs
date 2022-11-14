using Airdnd.ViewData;
using Airdnd.ViewModel;
using Airdnd.Web.Services.Partial;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Airdnd.Controllers
{
    public class UserWishListController : Controller
    {
        private readonly WishlistPartialService _wishlistPartialService;
        public UserWishListController(WishlistPartialService wishlistPartialService)
        {
            _wishlistPartialService = wishlistPartialService;
        }

        [Authorize]
        public IActionResult UserWishList()
        {
            int userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };
            var result = _wishlistPartialService.GetAllWishlists(userId);
            return View(result);
        }
    }
}
