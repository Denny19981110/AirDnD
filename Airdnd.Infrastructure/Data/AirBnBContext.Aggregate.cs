using Airdnd.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airdnd.Infrastructure.Data
{
    public partial class AirBnBContext
    {
        partial void OnModelCreatingPartial( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<PropertyGroup>().HasData(SeedData.PropertyGroups());
            modelBuilder.Entity<PropertyType>().HasData(SeedData.PropertyTypes());
            modelBuilder.Entity<PrivacyType>().HasData(SeedData.PrivacyTypes());
            modelBuilder.Entity<ServiceType>().HasData(SeedData.ServiceTypes());
            modelBuilder.Entity<Service>().HasData(SeedData.Services());
            modelBuilder.Entity<Legal>().HasData(SeedData.Legals());
            modelBuilder.Entity<Highlight>().HasData(SeedData.Highlights());
            modelBuilder.Entity<UserAccount>().HasData(SeedData.UserAccounts());
            modelBuilder.Entity<WishList>().HasData(SeedData.WishLists());
            modelBuilder.Entity<WishListDetail>().HasData(SeedData.WishListDetails());
            modelBuilder.Entity<Listing>().HasData(SeedData.Listings());
            modelBuilder.Entity<LegalListing>().HasData(SeedData.LegalListings());
            //modelBuilder.Entity<Order>().HasData(SeedData.Orders());
            modelBuilder.Entity<Comment>().HasData(SeedData.Comments());
            modelBuilder.Entity<Calendar>().HasData(SeedData.Calendars());
            modelBuilder.Entity<ServiceListing>().HasData(SeedData.service());
            modelBuilder.Entity<ListingImage>().HasData(SeedData.ListingsImg());
            modelBuilder.Entity<Rating>().HasData(SeedData.Rating());
        }
    }
}
