using Airdnd.Core.Entities;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Models.DtoModels.RoomSource;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace AirDnd.Controllers
{
    public class RoomSourceController : Controller
    {
        private readonly RoomSourceService _roomSourceService;
        int? userId = null;

        public RoomSourceController(RoomSourceService context)
        {
            _roomSourceService = context;
        }

        [Authorize]
        public IActionResult Form()
        {
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };

            var result = new RoomSourceViewModel
            {
                PropertyGroup = _roomSourceService.GetPropertyGroups().Select(x => new RoomSourceViewModel.Page1 { PropertyGroupId = x.PropertyGroupId, PropertyGroupName = x.PropertyGroupName, PropertyGroupImgUrl = x.PropertyGroupImgUrl }),
                PropertyType = _roomSourceService.GetPropertyTypes().Select(x => new RoomSourceViewModel.Page2 { PropertyTypeId = x.PropertyTypeId, PropertyGroupId = x.PropertyGroupId, PropertyTitle = x.PropertyTitle, PropertyContent = x.PropertyContent }),
                PrivacyType = _roomSourceService.GetPrivacyTypes().Select(x => new RoomSourceViewModel.Page3 { PrivacyTypeId = x.PrivacyTypeId, PrivacyTypeName = x.PrivacyTypeName }),
                Service = _roomSourceService.GetServices().Select(x => new RoomSourceViewModel.Page6
                {
                    ServiceTypeId = x.ServiceTypeId,
                    ServiceTypeName = x.ServiceTypeName,
                    ServiceItems = x.ServiceItems.Select(y => new RoomSourceViewModel.Page6.ServiceItem { ServiceId = y.ServiceId, Service = y.Service, ServiceIconPath = y.ServiceIconPath, ServiceTypeId = y.ServiceTypeId, Sort = y.Sort })
                }),
                HighLight = _roomSourceService.GetHighLights().Select(x => new RoomSourceViewModel.Page9 { HighLightId = x.HighLightId, HighLightName = x.HighLightName, HighLightIconPath = x.HighLightIconPath }),
                Legal = _roomSourceService.GetLegals().Select(x => new RoomSourceViewModel.Page12 { LegalId = x.LegalId, LegalName = x.LegalName }),
                UserName = _roomSourceService.GetPersonName((int)userId),
            };
            return View(result);
        }
        [Authorize]
        public IActionResult Index()
        {
            
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateRoom(CreateRoomDto query, string[] File)
        {
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };
            _roomSourceService.CreateRoom(query, File, (int)userId);
            return RedirectToAction("Index", "ManagingRoom");
            
        }
    }
}
