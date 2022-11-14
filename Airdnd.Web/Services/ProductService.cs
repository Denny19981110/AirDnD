using Airdnd.Core.Entities;
using Airdnd.Core.enums;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Interfaces;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Base;
using Airdnd.Web.ViewModels.Partial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Listing> _listing;
        private readonly IRepository<WishList> _wishList;
        private readonly IRepository<WishListDetail> _wishlistDetail;
        private readonly IRepository<ListingImage> _listingImage;
        private readonly IRepository<PropertyType> _propertyType;
        private readonly IRepository<ServiceListing> _service;
        private readonly IRepository<PrivacyType> _privacy;
        private readonly IRepository<Calendar> _calendar;
        private readonly IRepository<Rating> _rating;
        private readonly HomeService _homeService;

        public ProductService(IRepository<Listing> listing, IRepository<WishListDetail> wishlistDetail, IRepository<ListingImage> listingImage, IRepository<WishList> wishList, IRepository<PropertyType> propertyType, IRepository<ServiceListing> service, IRepository<PrivacyType> privacy, IRepository<Calendar> calendar, HomeService homeService, IRepository<Rating> rating)
        {
            _listing = listing;
            _listingImage = listingImage;
            _wishlistDetail = wishlistDetail;
            _wishList = wishList;
            _propertyType = propertyType;
            _service = service;
            _privacy = privacy;
            _calendar = calendar;
            _homeService = homeService;
            _rating = rating;
        }
        public IEnumerable<ProductDto> Get16Listings(int? loginUserId, int page)
        {
            var allowProduct = _listing.GetAllReadOnly().Where(l => l.Status == StatusType.HasUpload).ToList();
            var products = _homeService.SkipListings(page, allowProduct);

            var imgs = _listingImage.GetAllReadOnly().ToList();
            var properties = _propertyType.GetAllReadOnly().ToList();
            var ratings = _rating.GetAllReadOnly().GroupBy(r => r.ListingId).Select(r => new { ListingId = r.Key, RatingAvg = r.Average(l => l.RatingAvg) }).ToList();

            var service = _service.GetAllReadOnly().ToList();
            var privacy = _privacy.GetAllReadOnly().ToList();
            var notAvaibleDate = _calendar.GetAllReadOnly().Where(c => c.Available == false).ToList().Select(c => new { Listing = c.ListingId, Date = c.CalendarDate });

            var result = products.Select(p => new ProductDto
            {
                Id = p.ListingId,
                ListingName = p.ListingName,
                Price = p.DefaultPrice,
                Location = new Location { Lat = p.Lat, Lng = p.Lng },
                Rating = ratings.FirstOrDefault(r => r.ListingId == p.ListingId)?.RatingAvg?.ToString("0.0") ?? "最新",
                ImgPath = imgs.Where(i => i.ListingId == p.ListingId).Select(i => i.ListingImagePath).ToList(),
                IsWish = false,
                PropertyTitle = properties.Where(prop => prop.PropertyId == p.PropertyId).Select(prop => prop.PropertyName).First(),
                PropertyId = p.PropertyId,
                ServiceId = service.Where(s => s.ListingId == p.ListingId).Select(s => s.ServiceId),
                PrivacyId = p.CategoryId,
                Rooms = new Rooms { Bedrooms = p.BedRoom, Beds = p.Bed, Bathrooms = p.BathRoom },
                NotAvailableDate = notAvaibleDate.Where(d => d.Listing == p.ListingId)?.Select(d => d.Date)
            }).ToList();

            return result;
        }

        public IEnumerable<ProductDto> GetAllListings(int? loginUserId)
        {
            IEnumerable<int> wdLists = new List<int>();
            if (loginUserId != 0)
            {
                var userWishlists = _wishList.GetAllReadOnly().Where(w => w.UserAccountId == loginUserId).ToList().Select(w => w.WishlistId);
                wdLists = _wishlistDetail.GetAllReadOnly().Where(wd => userWishlists.Contains(wd.WishlistId)).ToList().Select(w => w.ListingId).ToList();
            }
            var products = _listing.GetAllReadOnly().Where(l => l.Status == StatusType.HasUpload).ToList();


            var imgs = _listingImage.GetAllReadOnly().ToList();
            var properties = _propertyType.GetAllReadOnly().ToList();
            var ratings = _rating.GetAllReadOnly().GroupBy(r => r.ListingId).ToList().Select(r => new { ListingId = r.Key, RatingAvg = r.Average(l => l.RatingAvg) }).ToList();

            var service = _service.GetAllReadOnly().ToList();
            var privacy = _privacy.GetAllReadOnly().ToList();
            var notAvaibleDate = _calendar.GetAllReadOnly().ToList().Where(c => c.Available == false).Select(c => new { Listing = c.ListingId, Date = c.CalendarDate });

            var result = products.Select(p => new ProductDto
            {
                Id = p.ListingId,
                ListingName = p.ListingName,
                Price = p.DefaultPrice,
                Location = new Location { Lat = p.Lat, Lng = p.Lng },
                Rating = ratings.FirstOrDefault(r => r.ListingId == p.ListingId)?.RatingAvg?.ToString("0.0") ?? "最新",
                ImgPath = imgs.Where(i => i.ListingId == p.ListingId).Select(i => i.ListingImagePath).ToList(),
                IsWish = false,
                PropertyTitle = properties.Where(prop => prop.PropertyId == p.PropertyId).Select(prop => prop.PropertyName).First(),
                PropertyId = p.PropertyId,
                ServiceId = service.Where(s => s.ListingId == p.ListingId).Select(s => s.ServiceId),
                PrivacyId = p.CategoryId,
                Rooms = new Rooms { Bedrooms = p.BedRoom, Beds = p.Bed, Bathrooms = p.BathRoom },
                NotAvailableDate = notAvaibleDate.Where(d => d.Listing == p.ListingId)?.Select(d => d.Date)
            }).ToList();

            if (loginUserId.HasValue)
            {
                foreach (var product in result)
                {
                    product.IsWish = wdLists.Any(w => w == product.Id);
                }
            }
            return result;
        }
        public IEnumerable<HomePropertyDto> GetHomeProperties()
        {
            var listings = _listing.GetAllReadOnly().ToList();
            var props = _propertyType.GetAllReadOnly().Where(p => p.IconPath != "" && p.IconPath != "。").ToList()
            .Select(p => new
            {
                Props = p,
                listingCount = listings.Count(l => l.PropertyId == p.PropertyId)
            });

            var prop = props.OrderByDescending(p => p.listingCount).Select(p => new HomePropertyDto()
            {
                Id = p.Props.PropertyId,
                PropertyTitle = p.Props.PropertyName,
                IconPath = p.Props.IconPath
            }).ToList();
            var result = prop.GroupBy(p => p.PropertyTitle).Select(gp => gp.First());
            return result;
        }
    }
}
