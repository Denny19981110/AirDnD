using Airdnd.Admin.Enums;
using Airdnd.Admin.Models;
using Airdnd.Admin.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Airdnd.Admin.WebApi
{
    public class ListingApiController :BaseApiController
    {
        private readonly ListingManageService _service;

        public ListingApiController( ListingManageService service )
        {
            _service = service;
        }
        [HttpGet]
        [Authorize]
        public IActionResult ListingManage()
        {
            var allListings = _service.GetAllListings();
            return new JsonResult(allListings);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus( [FromBody] ListingCondition listing )
        {
            int result = await _service.switchListingStatus(listing.ListingId, listing.Status);
            if( result > 0 )
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("修改失敗");
            }
        }
    }
}
