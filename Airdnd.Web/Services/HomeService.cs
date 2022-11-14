using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Base;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Airdnd.Web.ViewModels.Partial;
using Airdnd.Core.enums;
using Airdnd.Web.Interfaces;

namespace Airdnd.Web.Services
{
    public class HomeService
    {
        private readonly IRepository<Listing> _listing;
        private readonly IRepository<WishList> _wishList;
        private readonly IRepository<WishListDetail> _wishlistDetail;
        private readonly IRepository<ListingImage> _listingImage;
        private readonly IRepository<PropertyType> _propertyType;
        private readonly IRepository<UserAccount> _userAccount;
        private readonly IRepository<Rating> _rating;
        private readonly RankingService _rankingService;

        public HomeService( IRepository<Listing> listing, IRepository<Comment> comment, IRepository<WishListDetail> wishlistDetail, IRepository<ListingImage> listingImage, IRepository<UserAccount> userAccount, IRepository<WishList> wishList, IRepository<PropertyType> propertyType, IRepository<Rating> rating, RankingService rankingService )
        {
            _listing = listing;
            _listingImage = listingImage;
            _wishlistDetail = wishlistDetail;
            _wishList = wishList;
            _propertyType = propertyType;
            _userAccount = userAccount;
            _rating = rating;
            _rankingService = rankingService;
        }
        public UserInfoDto GetUserInfo( int? userId )
        {
            if( userId == 0 )
            {
                return null;
            }
            else
            {
                var getUser = _userAccount.GetAllReadOnly().First(u => u.UserAccountId == userId);

                var user = new UserInfoDto
                { UserId = getUser.UserAccountId };
                if( getUser.Lat != 0 && getUser.Lat.HasValue )
                {
                    user.UserLocation = new Location { Lat = (double)getUser.Lat, Lng = (double)getUser.Lng };
                }
                else
                {
                    user.UserLocation = new Location { Lat = 90, Lng = 0 };
                }
                return user;
            }

        }
        public IEnumerable<ProductDto> SetIsWish( int userId, IEnumerable<ProductDto> listings )
        {

            var userWishlists = _wishList.GetAllReadOnly().Where(w => w.UserAccountId == userId).ToList().Select(w => w.WishlistId);
            var wdLists = _wishlistDetail.GetAllReadOnly().Where(wd => userWishlists.Contains(wd.WishlistId)).ToList().Select(w => w.ListingId).ToList();

            var products = listings.ToList();
            foreach( var product in products )
            {
                product.IsWish = wdLists.Any(w => w == product.Id);
            }
            return products;
        }
        public IEnumerable<ProductDto> GetListingByProperty( int propertyId, int? loginUserId )
        {
            IEnumerable<int> wdLists = new List<int>();
            if( loginUserId.HasValue )
            {
                var userWishlists = _wishList.GetAllReadOnly().Where(w => w.UserAccountId == loginUserId).ToList().Select(w => w.WishlistId);
                wdLists = _wishlistDetail.GetAllReadOnly().Where(wd => userWishlists.Contains(wd.WishlistId)).ToList().Select(w => w.ListingId).ToList();

            }

            var ratings = _rating.GetAllReadOnly().GroupBy(r => r.ListingId).Select(r => new { ListingId = r.Key, RatingAvg = r.Average(l => l.RatingAvg) }).ToList();

            var allProducts = _listing.GetAllReadOnly().ToList();

            var imgs = _listingImage.GetAllReadOnly().ToList();
            var properties = _propertyType.GetAllReadOnly().ToList();
            var products = allProducts.Where(a => a.PropertyId == propertyId).Select(p => new ProductDto
            {
                Id = p.ListingId,
                ListingName = p.ListingName,
                Price = p.DefaultPrice,
                Location = new Location { Lat = p.Lat, Lng = p.Lng },
                Rating = ratings.FirstOrDefault(r => r.ListingId == p.ListingId)?.RatingAvg?.ToString("0.0") ?? "最新",
                ImgPath = imgs.Where(i => i.ListingId == p.ListingId).Select(i => i.ListingImagePath).ToList(),
                IsWish = false,
                PropertyTitle = properties.Where(prop => prop.PropertyId == p.PropertyId).Select(prop => prop.PropertyName).First()
            }).ToList();
            if( loginUserId.HasValue )
            {
                foreach( var product in products )
                {
                    product.IsWish = wdLists.Any(w => w == product.Id);
                }
            }
            return products;
        }
        public int CountAllPages()
        {
            var listingCount = _listing.GetAllReadOnly()
                                       .Where(l => l.Status == StatusType.HasUpload).ToList()
                                       .Select(l => l.ListingId)
                                       .ToList().Count;
            if( listingCount % 16 == 0 )
            {
                return listingCount / 16;
            }
            else
            {
                return (listingCount / 16) + 1;
            }
        }
        public List<Listing> SkipListings( int page, IEnumerable<Listing> listings )
        {
            int skipItems = (page - 1) * 16;
            var products = listings.Skip(skipItems).Take(16).ToList();

            return products;
        }
    }
}
