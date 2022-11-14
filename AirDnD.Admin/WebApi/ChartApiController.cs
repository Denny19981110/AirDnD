using Airdnd.Admin.Models.DtoModels;
using Airdnd.Admin.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airdnd.Admin.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChartApiController : ControllerBase
    {
        private readonly ChartService _chartService;

        public ChartApiController(ChartService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet]
        public IActionResult GetChartData()
        {
            var result = new ChartDto
            {
                TotalMembers = _chartService.GetAllMembers(),
                ThisMonthOrders = _chartService.GetThisMonthOrders(),
                ThisYearOrders = _chartService.GetThisYearOrders(),
                ThisMonthRevenue = _chartService.GetThisMonthRevenue(),
                ThisYearRevenue = _chartService.GetThisYearRevenue(),
                EveryMonthOrders = _chartService.GetEveryMonthOrders(),
                EveryMonthRevenue = _chartService.GetEvertMonthRevenue(),
                EverySeasonMembers = _chartService.GetEverySeasonMember(),
                EverySeasonListing = _chartService.GetEverySeasonRoomSource(),
                MembersGender = _chartService.GetMemberGender()
            };
            return new JsonResult(result);
        }
    }
}
