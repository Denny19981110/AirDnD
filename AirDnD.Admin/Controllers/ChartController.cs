using Microsoft.AspNetCore.Mvc;

namespace Airdnd.Admin.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult DataAnalysis()
        {
            return View();
        }
    }
}
