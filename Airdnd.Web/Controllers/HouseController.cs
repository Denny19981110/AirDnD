using Airdnd.Core.Entities;
using Airdnd.Web.Models;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Hoster;
using Airdnd.Web.ViewModels.House;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace Airdnd.Controllers
{
    public class HouseController : Controller
    {
        private readonly HostService _hostService;
        private readonly RankingService _rankingService;
        private readonly HouseService _houseService;
        private readonly ExpenseService _expenseService;
        public HouseController(HostService hostService, ExpenseService expenseService, HouseService houseService, RankingService rankingService)
        {
            _hostService = hostService;
            _houseService = houseService;
            _rankingService = rankingService;
            _expenseService=expenseService;
        }

        public IActionResult Host(int id=1)
        {
            if (!_hostService.HouseExist(id))
            {
                return RedirectToAction("Index", "Home");
            }
            else {
                var user = _hostService.GetHost(id);
                ViewData["user"] = user;
                SeoHelpDto seohelp = new SeoHelpDto
                {
                    WebAddress = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value.ToString()}/Host/User{user.HostID}",
                    Title = $"我是優質房東{user.HostName}",
                    Description = $"{user.HostAboutMe}-來AirDnD了解更多關於{user.HostName}的故事",
                    Image = user.AvatarUrl,
                };

                ViewData["seohelp"] = seohelp;
                return View();
            }
            
        }
        public IActionResult Index(string name= "他們說我是有用的年輕人")
        {
            if (!_houseService.HouseExist(name))
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                int userId = 0;
                if (User.Identity.IsAuthenticated)
                {
                    userId = int.Parse(User.Identity.Name);
                };
                ViewData["userId"] = userId;

                var id = _houseService.SearchHouseID(name);
                List<string> disableDay;
                if (_expenseService.AnyDisable(id))
                {
                    disableDay = _expenseService.GetDisableDays(id);
                }
                else
                {
                    disableDay = null;
                }
                
                if (_expenseService.IsSpecialPrice(id))
                {
                    var EepensiveDays = _expenseService.GetEepensiveDays(id);
                    var CheapDays = _expenseService.GetCheapDays(id);
                    SpecialPriceDto specialPrice = new SpecialPriceDto
                    {
                        cheapDays = CheapDays,
                        expensiveDays = EepensiveDays
                    };
                    ViewData["specialPrice"]=specialPrice;
                }
                else
                {
                    ViewData["specialPrice"] = null;
                }
                var cm = new HouseViewModel()
                {
                    IsComment = _rankingService.CommentExist(id)
                };

                if (cm.IsComment)
                {
                    cm.count = _rankingService.CommentNum(id);
                    cm.RatingAvg=(double)_rankingService.CommentAverage(id);
                    cm.RankPerson = _rankingService.CommentUser(id);
                    cm.RankSix = _rankingService.CommentSix(id);
                }
                var house = _houseService.GetHouse(id);
                var service= _houseService.GetService(id);
                SeoHelpDto seohelp = new SeoHelpDto
                {
                    Title = $"{house.HouseName}-{house.Category}{house.Property}",
                    Description = $"{house.Description}-來AirDnD了解更多關於{house.HouseName}的故事",
                    Image = house.HousePic[0].AvatarUrl,
                    WebAddress = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value.ToString()}/House/{house.HouseName}"
                };
                ViewData["disableDay"] = disableDay;
                ViewData["House"] = house;
                ViewData["service"] = service;
                ViewData["comment"] = cm;
                ViewData["seohelp"] = seohelp;
                return View();
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> Index([FromBody] OrderDate order)
        //{
        //    Console.WriteLine(order.startDate);
        //    Console.WriteLine(order.lastDate);
        //    Console.WriteLine(order.total);
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult Tst()
        //{
        //    if (TempData.ContainsKey("order"))
        //    {
        //        TempData.Keep();
        //        //把TEMPDATA轉回物件
        //        ViewData["order"] = System.Text.Json.JsonSerializer.Deserialize<OrderDateVM>(TempData["order"].ToString());
        //    }

        //    return View();
        //}

        //[HttpPost]
        //[Consumes("application/json")]
        //public IActionResult Tst([FromBody]OrderDateVM od)
        //{
        //    //這一端接受post來的字串
        //    TempData.Keep();
        //    //TEMPDATA只接受字串,但我傳進來的是物件(OrderDate od),所以要再去轉為JSON
        //    TempData["order"] = System.Text.Json.JsonSerializer.Serialize(od);
        //    TempData.Keep();
        //    return Ok();
        //}

    }
}
