using Airdnd.Core.Entities;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Base;
using Airdnd.Web.ViewModels.Partial;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;


namespace Airdnd.Controllers
{

    public class WishlistController :Controller
    {
        private readonly WishlistService _wishlistService;
        public WishlistController( WishlistService wishlistService )
        {
            _wishlistService = wishlistService;
        }
        [Authorize]
        [Route("wish/{id}")]
        [HttpGet]
        public IActionResult Index( int id )
        {
            var userId = 0;

            if( User.Identity.IsAuthenticated )
            {
                userId = int.Parse(User.Identity.Name);
            };
            var wishlist = _wishlistService.GetById(id);
            ViewBag.userId = userId;

            return View(wishlist);
        }
    }
}
