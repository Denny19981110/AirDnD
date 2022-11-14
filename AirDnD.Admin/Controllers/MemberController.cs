using Microsoft.AspNetCore.Mvc;

namespace Airdnd.Admin.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
