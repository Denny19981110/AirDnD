using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models;
using Airdnd.Web.ViewModels.House;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Calendar = Airdnd.Core.Entities.Calendar;

namespace Airdnd.Web.Services
{
    public class ExpenseService
    {
        private readonly IRepository<Listing> _listrepo;
        private readonly IRepository<Calendar> _calendar;
        private readonly IRepository<Order> _order;
        public ExpenseService(
            IRepository<Listing> listrepo,
            IRepository<Calendar> calendar,
            IRepository<Order> order
            )
        {
            _listrepo = listrepo;
            _calendar = calendar;
            _order = order;
        }
        public bool AnyDisable(int id)
        {
            var down = _calendar.GetAll().Any(x => x.ListingId == id && x.Available == false);
            var order = _order.GetAll().Any(x => x.ListingId == id == false);
            if (!down && !order)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public List<string> GetDisableDays(int id)
        {
            List<string> down = GetDownDays(id);
            List<string> book = GetBookedDays(id);
            if (down.Count > 0)
            {
                down.AddRange(book);
                return down;
            }
            else
            {
                book.AddRange(down);
                return book;
            }
        }
        public List<string> GetDownDays(int id)
        {
            return _calendar.GetAll().Where(x => x.ListingId == id && x.Available == false).Select(x => x.CalendarDate.ToString("yyyy-MM-dd")).ToList();

        }
        public List<string> GetBookedDays(int id)
        {
            List<Disable> abc = _order.GetAll().Where(x => x.ListingId == id).Select(x => new Disable
            {
                CheckInDate = x.CheckInDate,
                FinishDate = x.FinishDate,
                StayNight = x.StayNight
            }).OrderBy(x => x.CheckInDate).ToList();
            List<string> days = new List<string>();
            foreach (Disable d in abc)
            {
                for (var i = 0; i < d.StayNight; i++)
                {
                    days.Add(d.CheckInDate.AddDays(i).ToString("yyyy-MM-dd"));
                };
            };
            return days;
        }
        public decimal GetDefaultPrice(int id)
        {
           return _listrepo.GetAll().FirstOrDefault(x => x.ListingId == id).DefaultPrice;
        }
        public bool IsSpecialPrice(int id)
        {
            return _calendar.GetAll().Any(x => x.ListingId == id && x.Available != false);
        }

        public List<specialPrice> GetEepensiveDays(int id)
        {
            var averagePrice = GetDefaultPrice(id);
            List<specialPrice> EepensiveDays = _calendar.GetAll().Where(x => x.ListingId == id && x.Available != false && x.Price>averagePrice).Select(x => new specialPrice{ 
                price=x.Price,
                datetime= $"{x.CalendarDate.Year}-{x.CalendarDate.Month}-{x.CalendarDate.Day}",
            }).ToList();
            if (EepensiveDays.Count>0)
            {
                return EepensiveDays.OrderBy(x => x.datetime).ToList();
            }
            else
            {
                return null;
            }
        }
        public List<specialPrice> GetCheapDays(int id)
        {
            var averagePrice = GetDefaultPrice(id);
            List<specialPrice> CheapDays = _calendar.GetAll()
            .Where(x => x.ListingId == id && x.Available != false && x.Price < averagePrice).Select(x => new specialPrice
            {
                price = x.Price,
                datetime = $"{x.CalendarDate.Year}-{x.CalendarDate.Month}-{x.CalendarDate.Day}",
            }).ToList();
            if (CheapDays.Count > 0)
            {
                return CheapDays.OrderBy(x => x.datetime).ToList();
            }
            return null;
            
        }
    }

   
}

