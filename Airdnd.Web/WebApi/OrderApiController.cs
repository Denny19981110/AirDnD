using Airdnd.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Airdnd.Web.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly OrderService _orderService;
        int? userId = null;

        public OrderApiController(OrderService orderService)
        {
            _orderService = orderService;
        }
       /// <summary>
       /// This Api For Test
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            }
            var result = _orderService.GetRoomOrders((int)userId);
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult GetComing()
        {
            var today = DateTime.Now.Date;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            }
            var result = _orderService.GetRoomOrders((int)userId).Where(x => x.DateStart.Date <= today.AddDays(3) && x.DateStart.Date > today).Select(x => x);
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult GetCheckIn()
        {
            var today = DateTime.Now.Date;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            }
            var result = _orderService.GetRoomOrders((int)userId).Where(x => x.DateStart.Date <= today && today < x.DateEnd.Date).Select(x => x);
            return new JsonResult(result);
        }
        [HttpGet]
        public IActionResult GetCheckOut()
        {
            var today = DateTime.Now.Date;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            }
            var result = _orderService.GetRoomOrders((int)userId).Where(x => x.DateEnd.Date == today).Select(x => x);
            return new JsonResult(result);
        }
    }
}
