using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace AirDnd.Controllers
{
    public class CalendarController : Controller
    {
        private readonly CalendarService _calendarService;
        public CalendarController(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }
        [Authorize]
        public IActionResult Calendar()
        {            
            return View();
        }
    }
}
