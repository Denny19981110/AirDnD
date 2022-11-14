using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Infrastructure.Data;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Partial;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Airdnd.Web.Services.Partial
{
    public class WishlistPartialService
    {
        private readonly IRepository<WishList> _wishlistGroup;
        private readonly IRepository<WishListDetail> _wishlistDetail;
        private readonly IRepository<ListingImage> _listingImage;
        private readonly IDbConnection _dbConn;
        private readonly AirBnBContext _dbCtx;
        private readonly WishlistService _wishlistService;
        public WishlistPartialService( IRepository<WishList> wishlistGroup, IRepository<WishListDetail> wishlistDetail, IRepository<ListingImage> listingImage, IDbConnection dbConn, AirBnBContext dbCtx, WishlistService wishlistService )
        {
            _wishlistGroup = wishlistGroup;
            _wishlistDetail = wishlistDetail;
            _listingImage = listingImage;
            _dbConn = dbConn;
            _dbCtx = dbCtx;
            _wishlistService = wishlistService;
        }
        public IEnumerable<WishGroupPartialVM> GetAllWishlists( int? userId )
        {
            if( !userId.HasValue )
            {
                return null;
            }
            //找到所有的願望清單
            var wishlists = _wishlistGroup.GetAll().Where(w => w.UserAccountId == userId).ToList();

            //找到所有願望清單的第一間房
            var firstListings = _wishlistDetail.GetAll()
                .Where(wd => wishlists.Select(w => w.WishlistId).Contains(wd.WishlistId))
                .AsEnumerable()
                .GroupBy(wd => wd.ListingId)
                .Select(wd => wd.FirstOrDefault())
                .ToList();

            var imgs = _listingImage.GetAll()
                .Where(i => firstListings.Select(r => r.ListingId).Contains(i.ListingId)).ToList()
                .Select(x => new
                {
                    x.ListingImagePath,
                    WishListId = firstListings.Where(y => y.ListingId == x.ListingId).First().WishlistId
                })
                .ToList();

            var result = wishlists.Select(w => new WishGroupPartialVM
            {
                WishListId = w.WishlistId,
                GroupName = w.WishGroupName,
                Imgpath = imgs.FirstOrDefault(x => x.WishListId == w.WishlistId)?.ListingImagePath
                                                                                ?? "~/assert/common/airdnd_logo_snail.png",
            }).ToList();

            return result;
        }
        public async Task AddToWishlist( int wishlistId, int listingId )
        {
            await _wishlistDetail.AddAsync(
                  new WishListDetail { WishlistId = wishlistId, ListingId = listingId, CreatTime = DateTime.UtcNow });
        }
        public async Task AddNewWishlist( int userId, int listingId, string groupName )
        {
            using( var tran = _dbCtx.Database.BeginTransaction() )
            {
                _wishlistGroup.Add(new WishList { UserAccountId = userId, WishGroupName = groupName });

                await _wishlistDetail.AddAsync(new WishListDetail
                {
                    WishlistId = _wishlistGroup.GetAll().Where(w => w.UserAccountId == userId).ToList()
                                                         .OrderByDescending(w => w.WishlistId)
                                                         .Select(w => w.WishlistId)
                                                         .First(),
                    ListingId = listingId
                });
                tran.Commit();
            }

        }
        public async Task DeleteFromWishList( int userId, int listingId )
        {
            var wishDetail = _wishlistDetail.GetAll().Where(w => w.ListingId == listingId).ToList();

            var wishId = _wishlistGroup.GetAll().Where(w => wishDetail.Select(wd => wd.WishlistId)
                                        .Contains(w.WishlistId))
                                        .First(w => w.UserAccountId == userId).WishlistId;

            var result = wishDetail.First(w => w.WishlistId == wishId);

            await _wishlistDetail.DeleteAsync(result);


        }
    }
}
