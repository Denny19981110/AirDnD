using System;

namespace Airdnd.Web.ViewModels
{
    public class SignupViewModel
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }
    }
}
