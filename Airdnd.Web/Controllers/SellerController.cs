using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AirDnd.Controllers
{
    public class SellerController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
