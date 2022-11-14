using Airdnd.Core.enums;

namespace Airdnd.Web.Models.DtoModels
{
    public class MemberDataDto
    {
        public string Name { get; set; }
        public GenderType Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
