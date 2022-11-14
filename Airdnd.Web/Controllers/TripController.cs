using Airdnd.Web.Services;
using Airdnd.Web.ViewModels.Trip;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Airdnd.Web.Controllers
{
    public class TripController : Controller
    {
        private readonly TripService _tripService;

        public TripController(TripService tripService)
        {
            _tripService = tripService;
        }

        public IActionResult Index()
        {
            int customerId = 6;
            if (User.Identity.IsAuthenticated)
            {
                customerId = int.Parse(User.Identity.Name);
            };

            var trips = _tripService.GetById(customerId);

            return View(trips);
        }
    }
}
