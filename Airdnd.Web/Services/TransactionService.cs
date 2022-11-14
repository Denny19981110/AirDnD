using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Trip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.Services
{
    public class TransactionService
    {
        private readonly IRepository<Order> _order;
        private readonly IRepository<Listing> _listing;
        private readonly IRepository<ListingImage> _listingimage;

        public TransactionService(IRepository<Order> order, IRepository<Listing> listing, IRepository<ListingImage> listingimage)
        {
            _order = order;
            _listing = listing;
            _listingimage = listingimage;
        }


        public  IEnumerable<TransactionViewModel> GetAllTrans(int userId)
        {
            var listings = _listing.GetAllReadOnly().Where(x => x.UserAccountId == userId).ToList();
            var imgs = _listingimage.GetAllReadOnly().ToList();
            var result = new List<TransactionViewModel>();
            foreach(var listing in listings)
            {
                var trans = _order.GetAllReadOnly().Where(x => x.ListingId == listing.ListingId).Select(t => new TransactionViewModel
                {
                    ListingId = t.ListingId,
                    Startdate = t.CheckInDate,
                    Enddate = t.FinishDate,
                    price = t.UnitPrice,
                    Status = t.TranStatus,
                }).ToList();
                CheckOrderStatusHost(listing.ListingId);
                foreach (var item in trans)
                {
                    item.Listingname = listings.Where(x => x.ListingId == item.ListingId).First().ListingName;
                    item.PhotoUrl = imgs.Where(x => x.ListingId == item.ListingId).First().ListingImagePath;
                    result.Add(item);
                }
            }
            
            
            return result;
        }

        public void CheckOrderStatusHost(int listingId)
        {
            var orders = _order.GetAllReadOnly().Where(x => x.ListingId == listingId).ToList();
            foreach (var order in orders)
            {
                if (order.FinishDate < DateTime.Now)
                {
                    var temp = _order.Where(x=>x.OrderId == order.OrderId).First();
                    temp.Status = 2;
                    temp.TranStatus = 2;
                    _order.Update(temp);
                }
            }
        }
    }
}
