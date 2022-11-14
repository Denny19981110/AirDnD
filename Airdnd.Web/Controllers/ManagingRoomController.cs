using Airdnd.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airdnd.Web.Controllers
{
    public class ManagingRoomController : Controller
    {
        private readonly ManaginRoomService _managingroomservice;

        public ManagingRoomController(ManaginRoomService managingroomservice)
        {
            _managingroomservice = managingroomservice;
        }

        public  IActionResult Index()
        {
            var userId = 5;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };
            var listings = _managingroomservice.GetById(userId);


            string uri = HttpContext.Request.Host.Value.ToString();
            string url = HttpContext.Request.Scheme;
            foreach (var item in listings)
            {
                item.Url = $"{url}://{uri}/Roominfo/{item.RoomId}";
            }
            return View(listings);
        }
    }
}
