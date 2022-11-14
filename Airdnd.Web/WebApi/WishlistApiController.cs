using Airdnd.Core.Entities;
using Airdnd.Web.Models;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Services;
using Airdnd.Web.Services.Partial;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using static Airdnd.Web.Models.APIResult;

namespace Airdnd.Web.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistApiController :ControllerBase
    {
        private WishlistPartialService _wishlistPartialService;
        private WishlistService _wishlistService;

        public WishlistApiController( WishlistPartialService wishlistPartialService, WishlistService wishlistService )
        {
            _wishlistPartialService = wishlistPartialService;
            _wishlistService = wishlistService;
        }

        [HttpPost("AddListing")]
        public async Task<APIResult> AddListing( [FromBody] WishlistDto dto )
        {
            if( ModelState.IsValid )
            {
                await _wishlistPartialService.AddToWishlist(dto.WishlistId, dto.ListingId);
                return new APIResult(APIStatus.Success);
            }
            else
            {
                return new APIResult(APIStatus.Fail);
            }
        }
        [HttpPost("NewWishlist")]
        public async Task<APIResult> AddNewWishlist( [FromBody] WishlistDto dto )
        {
            var userId = 0;
            if( User.Identity.IsAuthenticated )
            {
                userId = int.Parse(User.Identity.Name);
            };
            if( ModelState.IsValid )
            {
                try
                {
                    await _wishlistPartialService.AddNewWishlist(userId, dto.ListingId, dto.GroupName);

                }
                catch( Exception err )
                {
                    return new APIResult(APIStatus.Fail, err.ToString());
                }
                return new APIResult(APIStatus.Success);
            }
            else
            {
                return new APIResult(APIStatus.Fail);
            }
        }
        [HttpDelete("DeleteListing")]
        public async Task<APIResult> DeleteListing( [FromBody] WishlistDto dto )
        {
            var userId = 0;
            if( User.Identity.IsAuthenticated )
            {
                userId = int.Parse(User.Identity.Name);
            };
            if( ModelState.IsValid )
            {
                try
                {
                    await _wishlistPartialService.DeleteFromWishList(userId, dto.ListingId);
                }
                catch( Exception err )
                {
                    return new APIResult(APIStatus.Fail, err.ToString());
                }
                return new APIResult(APIStatus.Success);
            }
            else
            {
                return new APIResult(APIStatus.Fail);
            }
        }
        [HttpPut("UpdateName")]
        public APIResult UpdateName( [FromBody] WishlistDto dto )
        {
            var userId = 0;
            if( User.Identity.IsAuthenticated )
            {
                userId = int.Parse(User.Identity.Name);
            };
            if( ModelState.IsValid )
            {
                try
                {
                    _wishlistService.UpdateWishlistName(userId, dto.WishlistId, dto.GroupName);
                }

                catch( Exception err )
                {
                    return new APIResult(APIStatus.Fail, err.ToString());
                }
                return new APIResult(APIStatus.Success);
            }
            else
            {
                return new APIResult(APIStatus.Fail);
            }
        }
        [HttpDelete("DeleteWishList")]
        public APIResult DeleteWishList( [FromBody] WishlistDto dto )
        {
            var userId = 0;
            if( User.Identity.IsAuthenticated )
            {
                userId = int.Parse(User.Identity.Name);
            };
            if( ModelState.IsValid )
            {
                try
                {
                    _wishlistService.DeleteWishlist(userId, dto.WishlistId);
                }
                catch( Exception err )
                {
                    return new APIResult(APIStatus.Fail, err.ToString());
                }
                return new APIResult(APIStatus.Success);
            }
            else
            {
                return new APIResult(APIStatus.Fail);
            }
        }
    }
    public class WishlistDto
    {
        public int ListingId { get; set; }
        public int WishlistId { get; set; }
        public string GroupName { get; set; }
    }
}
