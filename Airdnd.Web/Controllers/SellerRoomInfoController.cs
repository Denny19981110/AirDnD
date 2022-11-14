using Airbnb.ViewModel;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Models.DtoModels.RoomSource;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace AirDnd.Controllers
{
    public class SellerRoomInfoController : Controller
    {
        private readonly RoomSourceService _roomSourceService;
        public SellerRoomInfoController(RoomSourceService roomSourceService)
        {
            _roomSourceService = roomSourceService;
        }


        [HttpGet("roominfo/{id}")]
        [Authorize]
        public IActionResult SellerRoomInfo(int id)
        {

            //id = 4;

            var room =_roomSourceService.GetRoom(id);
            var result = new RoomInfoViewModel{
                Id = room.Id,
                RoomName = room.RoomName,
                Address = room.Address,
                People = room.People,
                Bed = room.Bed,
                BathRoom = room.BathRoom,
                BedRoom = room.BedRoom, 
                Description = room.Description,
                PrivacyType = room.PrivacyType,
                PropertyGroup = room.PropertyGroup,
                PropertyType = room.PropertyType,
                Photos = room.Photos,
            };
            return View(result);
        }
        [HttpPost]
        [Authorize]
        public IActionResult UpdateRoomInfo(UpdateRoomInfoDto input)
        {
            _roomSourceService.UpdateRoomInfo(input);

            return RedirectToAction("SellerRoomInfo", new {id = input.Id});
        }

        [HttpGet("roomimage/{id}")]
        [Authorize]
        public IActionResult RoomImagePage(int id)
        {
            var room = _roomSourceService.GetRoom(id);
            var result = new RoomInfoViewModel
            {
                Id = room.Id,
                Photos = room.Photos
            };
            return View(result);
        }

        [HttpPost]
        [Authorize]
        public IActionResult UploadAllImage(List<string> photos , int id)
        {
            _roomSourceService.ResetRoomImage(id, photos);

            return RedirectToAction("RoomImagePage", new { id = id });
        }

    }
}
