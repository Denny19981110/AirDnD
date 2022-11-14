using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Airdnd.Web.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalendarApiController : ControllerBase
    {
        private readonly CalendarService _calendarService;
        private readonly IRepository<Calendar> _efCalendar;
        int? userId = null;

        public CalendarApiController(CalendarService calendarService, IRepository<Calendar> efCalendar)
        {
            _calendarService = calendarService;
            _efCalendar = efCalendar;
        }

        [HttpGet]
        public IActionResult GetRoomSource()
        {
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };
            var result = _calendarService.GetRoomSelect((int)userId);
            return new JsonResult(result);
        }
        [HttpGet("{listingId}")]
        public IActionResult GetDefaultPrice(int listingId)
        {
            var result = _calendarService.GetDefaultPrice(listingId);
            return new JsonResult(result);
        }
        [HttpGet("{listingId}")]
        public IActionResult GetDates(int listingId)
        {
            var result = _calendarService.GetSpecifyDate(listingId).Select(x => new CalendarViewModel
            {
                Price = x.Price,
                Available = x.Available,
                CalendarDate = x.CalendarDate
            });
            return new JsonResult(result);
        }
        [HttpPost]
        public IActionResult MigrationCalendar([FromBody] CalendarDto query)
        {
            
            var DBinfo = _efCalendar.GetAll().FirstOrDefault(x => x.CalendarDate == query.CalendarDate && x.ListingId == query.ListingId);
            if(DBinfo == null)
            {
                _calendarService.CreateSpecifyDate(query);
            }
            else
            {
                _calendarService.UpdateSpecifyDate(query);
            }
            return Ok();
        }
    }
}
