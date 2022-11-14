using Microsoft.AspNetCore.Http;

namespace Airdnd.Web.ViewModels.UserImage
{
    public class UpdateImageViewModel
    {
        public IFormFile UserImage { get; set; }
    }
}
