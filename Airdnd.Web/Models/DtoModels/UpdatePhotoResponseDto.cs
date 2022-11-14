using System.Collections.Generic;

namespace Airdnd.Web.Models.DtoModels
{
    public class UpdatePhotoResponseDto
    {
        public List<string> PhotoList { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
