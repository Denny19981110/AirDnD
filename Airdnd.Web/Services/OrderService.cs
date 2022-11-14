using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models.DtoModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.Services
{
    public class OrderService
    {
        private IRepository<Order> _efOrder;
        private IRepository<UserAccount> _efUserAccount;
        private IRepository<Listing> _efListing;

        public OrderService(IRepository<Order> efOrder, IRepository<UserAccount> efUserAccount, IRepository<Listing> efListing)
        {
            _efOrder = efOrder;
            _efUserAccount = efUserAccount;
            _efListing = efListing;
        }
        public IEnumerable<OrderDto> GetRoomOrders(int userId)
        {
            var listings = _efListing.GetAll().Where(x => x.UserAccountId == userId).Select(x => x.ListingId);
            var orders = _efOrder.GetAll().Where(x => listings.Any(y => y == x.ListingId)).Select(x => new OrderDto
            {
                OrderId = x.OrderId,
                Roomname = _efListing.GetAll().First(y => y.ListingId == x.ListingId).ListingName,
                CustomerName = _efUserAccount.GetAll().First(y => y.UserAccountId == x.CustomerId).Name,
                DateStart = x.CheckInDate,
                DateEnd = x.FinishDate,
                Totalprice = x.UnitPrice
            });
            return orders;
        }
    }
}
