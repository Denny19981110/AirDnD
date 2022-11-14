using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Booking.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public IActionResult Completed()
        {
            int userId = 6;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };

            var trans = _transactionService.GetAllTrans(userId);

            return View(trans);
        }
        public IActionResult Future()
        {
            int userId = 6;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };

            var trans = _transactionService.GetAllTrans(userId);

            return View(trans);
        }
        public IActionResult GrossEarning()
        {
            int userId = 6;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };

            var trans = _transactionService.GetAllTrans(userId);

            return View(trans);
        }
    }
}
