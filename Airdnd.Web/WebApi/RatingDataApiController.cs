using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Airdnd.Web.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingDataApiController : ControllerBase
    {
        private readonly IRepository<Rating> _rankrepo;

        public RatingDataApiController(IRepository<Rating> rankrepo)
        {
            _rankrepo = rankrepo;

        }

        [HttpPost]
        public IActionResult UpdateRatingData(DataDto dto)
        {

            var raking = new Rating
            {
                ListingId = dto.RoomId,
                UserId = dto.UserID,
                RatingAvg = dto.RatingAvg,
                Clean = dto.Clean,
                Precise = dto.Precise,
                Communication = dto.Communication,
                Location = dto.Location,
                CheckIn = dto.CheckIn,
                CostPrice = dto.CostPrice,
                CreatTime = DateTime.UtcNow,
                CommentContent = dto.CommentContent
            };
            _rankrepo.Add(raking);
            return Ok();
        }

        public class DataDto
        {
            public int RoomId { get; set; }
            public int UserID { get; set; }
            public float RatingAvg { get; set; }
            public int Clean { get; set; }

            public int Precise { get; set; }
            public int Communication { get; set; }
            public int Location { get; set; }
            public int CheckIn { get; set; }
            public int CostPrice { get; set; }
            public DateTime CreateTime { get; set; }

            public string CommentContent { get; set; }


        }
    }
}
