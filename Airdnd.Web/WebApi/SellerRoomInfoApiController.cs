using Airdnd.Web.Models.DtoModels.RoomSource;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SellerRoomInfoApiController : ControllerBase
    {
        private readonly RoomSourceService _roomSourceService;
        private readonly PhotoService _photoService;

        public SellerRoomInfoApiController(RoomSourceService roomSourceService, PhotoService photoService)
        {
            _roomSourceService = roomSourceService;
            _photoService = photoService;

        }
        [HttpPost]
        public IActionResult EditRoomImage([FromForm] List<IFormFile> file)
        {

            var result = _photoService.UploadPhotos(file);

            //var room = _roomSourceService.GetRoom(id);
            List<string> photoList = new List<string>();

            //var result = _photoService.UploadPhotos(file);
            if (result.IsSuccess == false)
            {
                return BadRequest("失敗");
            }
            else
            {
                photoList = result.PhotoList.ToList();
                return Ok(photoList);
            }

        }

        [HttpPost]
        public IActionResult DeleteImage([FromBody] string input)
        {
            _roomSourceService.DeleteImage(input);
            return Ok();
        }
    }
}
