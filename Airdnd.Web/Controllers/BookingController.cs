using Airdnd.Core.Entities;
using Airdnd.Core.Helper;
using Airdnd.Core.Interfaces;
using Airdnd.Infrastructure.Data;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airdnd.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingService _bookingService;
        private readonly RankingService _rankingService;

        public BookingController(BookingService bookingService, RankingService rankingService)
        {
            _bookingService = bookingService;
            _rankingService = rankingService;
        }


        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Index([FromBody] OrderDateVM od)
        {
            //這一端接受post來的字串
            TempData.Keep();
            //TEMPDATA只接受字串,但我傳進來的是物件(OrderDate od),所以要再去轉為JSON
            TempData["order"] = System.Text.Json.JsonSerializer.Serialize(od);
            TempData.Keep();
            return Ok();
        }

        //[Authorize]
        public IActionResult Index()
        {
            OrderDateVM orderdateVM = new OrderDateVM();
            if (TempData.ContainsKey("order"))
            {
                TempData.Keep();
                //把TEMPDATA轉回物件
                orderdateVM = System.Text.Json.JsonSerializer.Deserialize<OrderDateVM>(TempData["order"].ToString());
            }

            int customerId = 6;
            if (User.Identity.IsAuthenticated)
            {
                customerId = int.Parse(User.Identity.Name);
            };

            int customer = orderdateVM.Adult + orderdateVM.Child;
            int staying = (orderdateVM.lastDate - orderdateVM.startDate).Days;

            var bookinginfo = _bookingService.GetOrderInfo(customerId, orderdateVM.houseID, orderdateVM.total);
            
            double? rating = 0;
            int comment = 0;
            var cm = _rankingService.CommentExist(orderdateVM.houseID);

            if (cm == true)
            {
                rating = _rankingService.CommentAverage(orderdateVM.houseID);
                comment = _rankingService.CommentNum(orderdateVM.houseID);
            }
            
            

            var bookingVM = new BookingViewModel
            {
                CustomerId = customerId,
                ListingId = orderdateVM.houseID,
                Startdate = orderdateVM.startDate,
                Enddate = orderdateVM.lastDate,
                Checkintime = "15:00",
                Customer = customer,
                Rating = rating,
                Comment = comment,
                Roomname = bookinginfo.Roomname,
                Roomdescription = bookinginfo.Roomdescription,
                Staying = staying,
                Cleaningfee = bookinginfo.Cleaningfee,
                Servicecharge = bookinginfo.Servicecharge,
                Stayingprice = orderdateVM.total,
                Children = orderdateVM.Child,
                Adults = orderdateVM.Adult,
                Infants = orderdateVM.Infant,
                RoomPhoto = bookinginfo.Roomphoto,
                Totalprice = bookinginfo.Servicecharge + bookinginfo.Cleaningfee + orderdateVM.total,
            };

            return View(bookingVM);
        }

        [HttpPost]
        public IActionResult Index(Order order)
        {
            
            _bookingService.CreateOrder(order);
            
            return RedirectToAction("ECPay",new ECPayOrderDto { payId=order.PayId, orderId=order.OrderId});
        }

        public IActionResult ECPay(ECPayOrderDto eCPayOrder)
        {
            var order = _bookingService.GetECPayOrder(eCPayOrder.orderId);
            int TotalPrice = (int)(order.Totalprice);
            string Orderid = (order.OrderId).ToString();
            string uri = HttpContext.Request.Host.Value.ToString();
            string url = HttpContext.Request.Scheme;
            var result = new Dictionary<string, string>
            {
                { "MerchantID",  "3002607"},
                { "MerchantTradeNo", $"{eCPayOrder.payId}" },
                { "MerchantTradeDate",  DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")},
                { "PaymentType",  "aio"},
                { "TotalAmount", $"{TotalPrice}" },
                { "TradeDesc",  "無"},
                { "ItemName", $"{order.Roomname}" },
                { "ReturnURL", $"{url}://{uri}/Trip" },
                { "OrderResultURL", $"{url}://{uri}/Booking/Booked" },
                { "ChoosePayment",  "ALL"},
                { "EncryptType",  "1"},
                { "CustomField1", $"{Orderid}" }
            };
            result["CheckMacValue"] = GetCheckMacValue(result);
            return View(result);
        }
        private string GetCheckMacValue(Dictionary<string, string> order)
        {
            var param = order.Keys.OrderBy(x => x).Select(key => key + "=" + order[key]).ToList();
            var checkValue = string.Join("&", param);

            var hashKey = "pwFHCqoQZGmho4w6";
            var HashIV = "EkRm7iFT261dpevs";

            checkValue = $"HashKey={hashKey}" + "&" + checkValue + $"&HashIV={HashIV}";
            checkValue = HttpUtility.UrlEncode(checkValue).ToLower();
            checkValue = Encryption.SHA256Encrypt(checkValue);


            return checkValue.ToUpper();
        }

        public IActionResult Booked(IFormCollection order)
        {
            int orderid = Convert.ToInt32(order["CustomField1"]);
            int code = Convert.ToInt32(order["RtnCode"]);

            if(code != 1)
            {
                return RedirectToAction("Index", "Trip");
            }
            else
            {
                _bookingService.ChangeOrderStatus(orderid);
                var bookorder = _bookingService.GetById(orderid);

                var result = new BookedViewModel
                {
                    Roomname = bookorder.Roomname,
                    RoomPhoto = bookorder.RoomPhoto,
                    Customer = bookorder.Customer,
                    Startdate = bookorder.Startdate,
                    Enddate = bookorder.Enddate,
                    Address = bookorder.Address,
                    Totalprice = bookorder.Totalprice,
                    Email = bookorder.Email,
                    OrderId = bookorder.PayId,
                };

                return View(result);
            }
            }
            

        public IActionResult ReBook()
        {

            int customerId = 6;
            if (User.Identity.IsAuthenticated)
            {
                customerId = int.Parse(User.Identity.Name);
            };
            int id = Convert.ToInt32(HttpContext.Request.Query["Id"]);
            var bookorder = _bookingService.GetRebookOrder(id);
            double? rating = 0;
            int comment = 0;
            var cm = _rankingService.CommentExist(bookorder.ListingId);

            if (cm == true)
            {
                rating = _rankingService.CommentAverage(bookorder.ListingId);
                comment = _rankingService.CommentNum(bookorder.ListingId);
            }

            var bookingVM = new BookingViewModel
            {
                CustomerId = customerId,
                ListingId = bookorder.ListingId,
                Startdate = bookorder.StartDate,
                Enddate = bookorder.EndDate,
                Checkintime = "15:00",
                Customer = bookorder.Customer,
                Rating = rating,
                Comment = comment,
                Roomname = bookorder.Roomname,
                Roomdescription = bookorder.Roomdescription,
                Staying = bookorder.Staying,
                Cleaningfee = bookorder.Cleaningfee,
                Servicecharge = bookorder.Servicecharge,
                Children = bookorder.Children,
                Adults = bookorder.Adults,
                Infants = bookorder.Infants,
                RoomPhoto = bookorder.Roomphoto,
                Totalprice = bookorder.TotalPrice,
                OrderId = id
            };

            return View(bookingVM);
        }
        [HttpPost]
        public IActionResult ReBook(Order order)
        {
            return RedirectToAction("ECPay", new ECPayOrderDto { payId = order.PayId, orderId = order.OrderId });
        }

        [HttpPost]
        public IActionResult Delete(Order order)
        {
            _bookingService.DeleteOrder(order);
            return RedirectToAction("Index", "Trip");
        }
    }
}
