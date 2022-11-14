using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Infrastructure.Data;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Airdnd.Web.ViewModels.Partial;
using Airdnd.Web.Interfaces;

namespace Airdnd.Web.Services
{
    public class SearchService :ISearchService
    {
        private readonly IRepository<Listing> _listing;
        private readonly IRepository<ListingImage> _listingImage;
        private readonly IRepository<PropertyType> _propertyType;
        private readonly IWishlistQuery _wishlistQuery;
        private readonly IRepository<ServiceListing> _service;
        private readonly IRepository<Calendar> _calendar;
        private readonly RankingService _rankingService;
        private readonly IRepository<Rating> _rating;


        public SearchService( IRepository<Listing> listing, IRepository<ListingImage> listingImage, IRepository<PropertyType> propertyType, IWishlistQuery wishlistQuery, IRepository<ServiceListing> service, IRepository<Calendar> calendar, RankingService rankingService, IRepository<Rating> rating )
        {
            _listing = listing;
            _listingImage = listingImage;
            _propertyType = propertyType;
            _wishlistQuery = wishlistQuery;
            _service = service;
            _calendar = calendar;
            _rankingService = rankingService;
            _rating = rating;
        }
        public IEnumerable<ProductDto> GetListingByConditions( string location, int userId, string checkIn, string checkOut, int adult, int child )
        {
            var tempListing = _listing.GetAllReadOnly().ToList();
            var propType = _propertyType.GetAllReadOnly().ToList();
            var userWishlistIds = _wishlistQuery.GetWishListByUserId(userId).ToList().Select(w => w.WishlistId);
            var service = _service.GetAllReadOnly().ToList();

            var wdLists = _wishlistQuery.GetWishDetailByUserlistIds(userWishlistIds).ToList().Select(w => w.ListingId).ToList();
            List<Listing> room = new List<Listing>();
            if( location != "Any" )
            {
                room = tempListing.Where(l => l.Address.Contains(location) || l.ListingName.Contains(location)).ToList();
            }
            else
            {
                room = tempListing.ToList();
            }
            var imgs = _listingImage.GetAllReadOnly().ToList();
            var ratings = _rating.GetAllReadOnly().GroupBy(r => r.ListingId).Select(r => new { ListingId = r.Key, RatingAvg = r.Average(l => l.RatingAvg) }).ToList();
            var calendar = _calendar.GetAllReadOnly().Where(c => c.Available == false).ToList();
            IEnumerable<ProductDto> listing = room.Select(l => new ProductDto
            {
                Id = l.ListingId,
                County = l.Address.Split("-")[0],
                ServiceId = service.Where(s => s.ListingId == l.ListingId).Select(s => s.ServiceId),
                PropertyTitle = propType.First(p => p.PropertyId == l.PropertyId).PropertyName,
                ListingName = l.ListingName,
                Rooms = new Rooms { Bedrooms = l.BedRoom, Beds = l.Bed, Bathrooms = l.BathRoom },
                Price = l.DefaultPrice,
                Location = new Location { Lat = l.Lat, Lng = l.Lng },
                Expected = l.Expected,
                Rating = ratings.FirstOrDefault(r => r.ListingId == l.ListingId)?.RatingAvg?.ToString("0.0") ?? "最新",
                ImgPath = imgs.Where(i => i.ListingId == l.ListingId).Select(i => i.ListingImagePath).ToList(),
                IsWish = wdLists.Any(w => w == l.ListingId),
                NotAvailableDate = calendar.Where(c => c.ListingId == l.ListingId).ToList().Select(c => c.CalendarDate)
            });

            var result = listing.ToList();

            if( checkIn != "Any" && checkOut != "Any" )
            {
                DateTime checkInDate = Convert.ToDateTime(checkIn);
                DateTime checkOutDate = Convert.ToDateTime(checkOut);
                if( result.Count != 0 && result.Where(l => l.NotAvailableDate != null).Count() != 0 )
                    result = result.Where(l => !l.NotAvailableDate.Any(d => d >= checkInDate && d < checkOutDate)).ToList();
            }

            int guest = adult + child;
            if( guest == 1 )
            {
                return result;
            }
            else if( guest != 20 )
            {
                result = result.Where(r => r.Expected == guest).ToList();
            }
            else
            {
                result = result.Where(r => r.Expected > 20).ToList();
            }

            return result;
        }
        public int CountAllPages( IEnumerable<ProductDto> listings )
        {
            var listingCount = listings.Count();
            if( listingCount % 16 == 0 )
            {
                return listingCount / 16;
            }
            else
            {
                return (listingCount / 16) + 1;
            }
        }
        public IEnumerable<ProductDto> Take16Listings( int page, IEnumerable<ProductDto> productDto )
        {
            int skipItems = (page - 1) * 16;
            return productDto.Skip(skipItems).Take(16).ToList();
        }
    }
}
