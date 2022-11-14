using Airdnd.Core.enums;

namespace Airdnd.Web.Models.DtoModels
{
    public class SocialLoginInputDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public LoginType LoginType { get; set; }
    }
}
