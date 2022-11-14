using Microsoft.AspNetCore.Mvc;

namespace Airdnd.Admin.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
