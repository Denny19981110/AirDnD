using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airdnd.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly LoginService _loginService;

        public MemberController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [Authorize]
        public IActionResult Member()
        {
            var userId = _loginService.GetCurrentUserId();
            return View(userId);
        }

        [Authorize]
        public IActionResult Safety()
        {
            var userId = _loginService.GetCurrentUserId();
            return View();
        }
    }
}
