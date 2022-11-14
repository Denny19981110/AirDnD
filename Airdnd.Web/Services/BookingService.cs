using Airdnd.Core.Entities;
using Airdnd.Core.Helper;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.ViewModels;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Airdnd.Web.Services
{
    public class BookingService
    {
        private readonly MailService _mailservice;
        private readonly IRepository<UserAccount> _useraccount;
        private readonly IRepository<Order> _order;
        private readonly IRepository<Listing> _listing;
        private readonly IRepository<ListingImage> _listingimage;

        public BookingService(IRepository<Order> order, IRepository<Listing> listing, IRepository<ListingImage> listingimage, IRepository<UserAccount> useraccount, MailService mailservice)
        {
            _order = order;
            _listing = listing;
            _listingimage = listingimage;
            _useraccount = useraccount;
            _mailservice = mailservice;
        }


        public OrderDto GetById(int orderid)
        {
            var orderinfo = _order.GetAll().First(x => x.OrderId == orderid);
            var orderlisting = _listing.GetAll().First(l => l.ListingId == orderinfo.ListingId);
            var orderimage = _listingimage.GetAll().First(i => i.ListingId == orderinfo.ListingId);
            var user = _useraccount.GetAll().First(u => u.UserAccountId == orderinfo.CustomerId);

            //email測試
            var email = user.Email;
            var userName = user.Name;
            _mailservice.SendOrderInfo(email);

            return new OrderDto
            {
                Roomname = orderlisting.ListingName,
                Startdate = orderinfo.CheckInDate.ToString("MM/dd"),
                Enddate = orderinfo.FinishDate.ToString("MM/dd"),
                Address = orderlisting.Address,
                Totalprice = orderinfo.UnitPrice,
                Customer = (int)(orderinfo.Adults ),
                Email = user.Email,
                RoomPhoto = orderimage.ListingImagePath,
                PayId = orderinfo.PayId
            };
        }
        public ECPayDto GetECPayOrder(int orderId)
        {
            var orderinfo = _order.GetAll().First(x => x.OrderId == orderId);
            var orderlisting = _listing.GetAll().First(l => l.ListingId == orderinfo.ListingId);
            var user = _useraccount.GetAll().First(u => u.UserAccountId == orderinfo.CustomerId);

            return new ECPayDto
            {
                Roomname = orderlisting.ListingName,
                OrderId = orderinfo.OrderId,
                Totalprice = orderinfo.UnitPrice
            };

        }

        public OrderDetailDto GetOrderInfo(int customerId, int listingId,decimal roomprice)
        {
            var listinginfo = _listing.GetAll().First(x => x.ListingId == listingId);
            var orderimage = _listingimage.GetAll().First(x => x.ListingId == listingId);

            return new OrderDetailDto
            {
                Roomname = listinginfo.ListingName,
                Roomdescription = listinginfo.Description,
                Cleaningfee = roomprice * 0.1m,
                Servicecharge = roomprice * 0.1m,
                Roomphoto = orderimage.ListingImagePath
            };
        }
        public void ChangeOrderStatus(int orderId)
        {
            var order = _order.GetAll().Where(o => o.OrderId == orderId).First();
            if(order != null)
            {
                order.Status = 1;
                _order.Update(order);
            }
        }

        public void CreateOrder(Order order)
        {
            _order.Add(order);
        }

        public UnpayOrderDto GetRebookOrder(int id)
        {

            //新增重新下訂DTO
            var order = _order.GetAll().Where(o => o.OrderId == id).First();
            var listinginfo = _listing.GetAll().First(x => x.ListingId == order.ListingId);
            var orderimage = _listingimage.GetAll().First(x => x.ListingId == order.ListingId);

            return new UnpayOrderDto
            {
                Roomname = listinginfo.ListingName,
                Roomdescription = listinginfo.Description,
                Cleaningfee = order.UnitPrice * 0.1m,
                Servicecharge = order.UnitPrice * 0.1m,
                Roomphoto = orderimage.ListingImagePath,
                ListingId = order.ListingId,
                StartDate = order.CheckInDate,
                EndDate = order.FinishDate,
                Children = order.Children,
                Infants = order.Infants,
                Adults = order.Adults,
                TotalPrice = order.UnitPrice,
                Customer = order.Adults + order.Children
            };
        }
        public void DeleteOrder(Order order)
        {
            _order.Delete(order);
        }
    }
}
