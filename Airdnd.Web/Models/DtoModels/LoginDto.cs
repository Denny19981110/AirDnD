using Airdnd.Core.enums;

namespace Airdnd.Web.Models.DtoModels
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginType LoginType { get; set; }
    }
}
