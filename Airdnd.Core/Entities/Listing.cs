using Airdnd.Core.enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Listing
    {
        public Listing()
        {
            Calendars = new HashSet<Calendar>();
            Highlights = new HashSet<Highlight>();
            LegalListings = new HashSet<LegalListing>();
            ListingImages = new HashSet<ListingImage>();
            Orders = new HashSet<Order>();
            Promos = new HashSet<Promo>();
            Ratings = new HashSet<Rating>();
            RecommendListings = new HashSet<RecommendListing>();
            ServiceListings = new HashSet<ServiceListing>();
            WishListDetails = new HashSet<WishListDetail>();
        }

        public int ListingId { get; set; }
        public decimal DefaultPrice { get; set; }
        public string Address { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public int PropertyId { get; set; }
        public int CategoryId { get; set; }
        public int Expected { get; set; }
        public int UserAccountId { get; set; }
        public StatusType Status { get; set; }
        public int Bed { get; set; }
        public int BedRoom { get; set; }
        public int BathRoom { get; set; }
        public int Toilet { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? EditTime { get; set; }
        public bool IndieBathroom { get; set; }
        public int HighlightId { get; set; }

        public virtual PrivacyType Category { get; set; }
        public virtual Highlight Highlight { get; set; }
        public virtual PropertyType Property { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
        public virtual ICollection<Highlight> Highlights { get; set; }
        public virtual ICollection<LegalListing> LegalListings { get; set; }
        public virtual ICollection<ListingImage> ListingImages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Promo> Promos { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<RecommendListing> RecommendListings { get; set; }
        public virtual ICollection<ServiceListing> ServiceListings { get; set; }
        public virtual ICollection<WishListDetail> WishListDetails { get; set; }
    }
}
