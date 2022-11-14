using Airdnd.Core.enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            Comments = new HashSet<Comment>();
            Listings = new HashSet<Listing>();
            Orders = new HashSet<Order>();
            Ratings = new HashSet<Rating>();
            WishLists = new HashSet<WishList>();
        }

        public int UserAccountId { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public GenderType Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public string AboutMe { get; set; }
        public string Address { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public LoginType LoginType { get; set; }
        public string Country { get; set; }
        public DateTime? PasswordModifyDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
