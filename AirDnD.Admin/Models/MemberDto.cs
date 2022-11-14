using Airdnd.Core.enums;
using System;

namespace Airdnd.Admin.Models
{
    public class MemberDto
    {
        public int UserAccountId { get; set; }
        public int Gender { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CreateDate { get; set; }
        public string AboutMe { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }
        public bool Host { get; set; }
        public int spend { get; set; }
    }
}
