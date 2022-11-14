using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.ViewModels.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Airdnd.Web.Services
{
    public class TripService
    {
        private readonly IRepository<Order> _order;
        private readonly IRepository<Listing> _listing;
        private readonly IRepository<ListingImage> _listingimage;


        public TripService(IRepository<Order> order, IRepository<Listing> listing, IRepository<ListingImage> listingimage)
        {
            _order = order;
            _listing = listing;
            _listingimage = listingimage;
        }


        public IEnumerable<TripViewModel> GetById(int userId)
        {

            CheckOrderStatusCustomer(userId);
            var imgs = _listingimage.GetAllReadOnly().ToList();
            var lists = _listing.GetAllReadOnly().ToList();
            var order = _order.GetAllReadOnly().Where(x => x.CustomerId ==userId).Select(t => new TripViewModel
            {
                Id = t.ListingId,
                Startdate = t.CheckInDate,
                Enddate = t.FinishDate,
                Status = t.Status,
                orderId = t.OrderId,
                
            }).ToList();
            foreach(var item in order)
            {
                item.ListingName = lists.Where(x => x.ListingId == item.Id).First().ListingName;
                item.PhotoUrl = imgs.Where(x => x.ListingId == item.Id).First().ListingImagePath;
                item.Address = lists.Where(x => x.ListingId == item.Id).First().Address;
            }
            return order;
        }

        public void CheckOrderStatusCustomer(int userId)
        {
            var orders = _order.GetAllReadOnly().Where(x => x.CustomerId == userId).ToList();
            foreach(var order in orders)
            {
                if(order.FinishDate < DateTime.Now)
                {
                    order.Status = 2;
                    order.TranStatus = 2;
                    _order.Update(order);
                }
            }
        }
    }
}
