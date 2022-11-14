using Airbnb.ViewData;
using Airbnb.ViewModel;
using Airdnd.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Airdnd.Controllers
{
    public class UserAccountPageController : Controller
    {
        private readonly UserAccountService _userAccountService;
        public UserAccountPageController(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        [Authorize]
        public IActionResult Index()
        {
            int userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };
            var user = _userAccountService.GetUser(userId);

            user.Image ??= "https://res.cloudinary.com/dbp76raxc/image/upload/v1664589756/defaultIMG_rm314m.jpg";

            var result = new UserAccountPageViewModel
            {
                User = new UserAccountPageViewModel.UserAccountPageData
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    Image = user.Image
                }
            };
            return View(result);
        }
    }
}
