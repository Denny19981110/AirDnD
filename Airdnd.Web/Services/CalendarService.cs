using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.Services
{
    public class CalendarService
    {
        private readonly IRepository<Calendar> _efCalendar;
        private readonly IRepository<Listing> _efListing;

        public CalendarService(IRepository<Calendar> efCalendar, IRepository<Listing> eflisting)
        {
            _efCalendar = efCalendar;
            _efListing = eflisting;
        }
        public IEnumerable<CalendarDto.CalendarSelect> GetRoomSelect(int userId)
        {
            var info = _efListing.GetAll().Where(x => x.UserAccountId == userId).Select(x => new CalendarDto.CalendarSelect
            {
                ListingId = x.ListingId,
                ListingName = x.ListingName
            });
            return info;
        }
        public decimal GetDefaultPrice(int listingId)
        {
            var price = _efListing.GetAll().First(x => x.ListingId == listingId).DefaultPrice;
            return price;
        }
        public IEnumerable<CalendarDto> GetSpecifyDate(int listingId)
        {
            var info = _efCalendar.GetAll().Where(x => x.ListingId == listingId).Select(x => new CalendarDto
            {
                CalendarId = x.CalendarId,
                CalendarDate = x.CalendarDate,
                Available = x.Available,
                Price = x.Price
            }).ToList();

            return info;
        }
        public decimal GetRoomRate(int id)
        {
            var rate = _efListing.GetAll().First(x => x.ListingId == id).DefaultPrice;
            return rate;
        }
        public void CreateSpecifyDate(CalendarDto query)
        {
            var info = new Calendar
            {
                CalendarDate = query.CalendarDate,
                Price = query.Price,
                ListingId = query.ListingId,
                Available = query.Available,
                OrderId = query.OrderId
            };
            _efCalendar.Add(info);
        }
        public void UpdateSpecifyDate(CalendarDto query)
        {
            var specifyDate = _efCalendar.GetAll().First(x => x.CalendarDate == query.CalendarDate && x.ListingId == query.ListingId);
            specifyDate.Price = query.Price;
            specifyDate.Available = query.Available;
            _efCalendar.Update(specifyDate);
        }
        
    }
}
