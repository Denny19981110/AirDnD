using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Airdnd.Web.Models.DtoModels
{
    public class UpdatePhotoRequestDto
    {
        public List<IFormFile> File { get; set; }
    }
}
