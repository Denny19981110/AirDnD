using Airdnd.Web.ViewModels.UserImage;

namespace Airdnd.Web.Models.DtoModels
{
    public class UpdateUserDto 
    {
        public int Id { get; set; }
        public string AboutMe { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
    }
}
