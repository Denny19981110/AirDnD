using Airdnd.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Airdnd.Web.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadPhotoApiController : ControllerBase
    {
        private readonly PhotoService _photoService;

        public UploadPhotoApiController(PhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        public IActionResult ConvertPhotoUrl(IFormFile file)
        {
            var result = _photoService.UploadPhoto(file);
            return Ok(result);
        }

    }
}
