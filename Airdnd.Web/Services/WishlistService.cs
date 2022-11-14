using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Infrastructure.Data;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airdnd.Web.Services
{
    public class WishlistService
    {
        private readonly IRepository<Listing> _listing;
        private readonly IRepository<WishListDetail> _wishlistDetail;
        private readonly IRepository<WishList> _wishlistGroup;
        private readonly IRepository<ListingImage> _listingImage;
        private readonly IRepository<Comment> _comment;
        private readonly IRepository<PropertyType> _propertyType;
        private readonly IRepository<Order> _order;
        private readonly AirBnBContext _dbCtx;
        private readonly RankingService _rankingService;

        public WishlistService(IRepository<Listing> listing, IRepository<WishListDetail> wishlistDetail, IRepository<ListingImage> listingImage, IRepository<WishList> wishlistGroup, IRepository<Comment> comment, IRepository<PropertyType> propertyType, IRepository<Order> order, AirBnBContext dbCtx, RankingService rankingService)
        {
            _listing = listing;
            _wishlistDetail = wishlistDetail;
            _listingImage = listingImage;
            _wishlistGroup = wishlistGroup;
            _comment = comment;
            _propertyType = propertyType;
            _order = order;
            _dbCtx = dbCtx;
            _rankingService = rankingService;
        }
        public WishlistVM GetById(int selectedWishlistId)
        {

            var wishListings = _wishlistDetail.GetAllReadOnly().Where(w => w.WishlistId == selectedWishlistId).ToList();
                                                              

            var getGroup = _wishlistGroup.GetAllReadOnly().FirstOrDefault(x => x.WishlistId == selectedWishlistId);

            if (getGroup == null)
            {
                return null;
            }

            var listings = _listing.GetAllReadOnly().Where(l => wishListings.Select(w=>w.ListingId).Contains(l.ListingId)).ToList();
            var propertyTitles = _propertyType.GetAllReadOnly().Where(p => listings.Select(l => l.PropertyId)
                                                                .Contains(p.PropertyId))
                                            .Select(p => new { p.PropertyId, p.PropertyName });

            var wishDetails = listings.Select(l => new WishlistDetailVM
            {
                Id = l.ListingId,
                ListingName = l.ListingName,
                PropertyTitle = propertyTitles.FirstOrDefault(p => p.PropertyId == l.PropertyId).PropertyName,
                Beds = l.Bed,
                Bedrooms = l.BedRoom,
                Bathrooms = l.BathRoom,
                GuestExpected = l.Expected,
                Price = l.DefaultPrice,
                IsWish = true,
                Rating = _rankingService.CommentAverage(l.ListingId)?.ToString("0.0") ?? "最新",
                ImgPath = _listingImage.GetAllReadOnly().Where(i => i.ListingId == l.ListingId)
                                                .Select(i => i.ListingImagePath)
                                                .ToList(),
                Location = new Location { Lat = l.Lat, Lng = l.Lng },
                CreatTime = wishListings.Where(w=>w.ListingId == l.ListingId).Select(w=>w.CreatTime).First()
            }).OrderBy(w=>w.CreatTime).ToList();

            var wishlist = new WishlistVM
            {
                WishlistID = selectedWishlistId,
                WishlistName = getGroup.WishGroupName,
                WishlistDetail = wishDetails,
                UserAcountId = getGroup.UserAccountId
            };

            return wishlist;
        }
        public void UpdateWishlistName(int userId, int wishlistId, string newName)
        {
            var lists = _wishlistGroup.GetAll().ToList();
            var selectedWishlist = _wishlistGroup.GetAll().First(w => w.WishlistId == wishlistId && w.UserAccountId == userId);
            selectedWishlist.WishGroupName = newName;
            _wishlistGroup.Update(selectedWishlist);
        }
        public void DeleteWishlist(int userId, int wishlistId)
        {
            using (var tran = _dbCtx.Database.BeginTransaction())
            {
                var selectedWishlist = _wishlistGroup.GetAll().First(w => w.WishlistId == wishlistId && w.UserAccountId == userId);
                var listDetail = _wishlistDetail.GetAll().Where(w => w.WishlistId == wishlistId).ToList();
                _wishlistDetail.DeleteRange(listDetail);

                _wishlistGroup.Delete(selectedWishlist);

                tran.Commit();
            }
        }
    }

}

