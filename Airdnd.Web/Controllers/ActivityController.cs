using Airdnd.Web.Models;
using Airdnd.Web.Services.Partial;
using Airdnd.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Airdnd.Web.Controllers
{
	public class ActivityController : Controller
     
	{
        private readonly MemberService _memberService;
        public ActivityController(MemberService memberService)
        {
            _memberService=memberService;
        }
        public IActionResult Index()
		{
            SeoHelpDto seohelp = new SeoHelpDto
            {
                WebAddress = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value.ToString()}/Hallow",
                Title = "萬聖節心理測驗：你有多邪惡？",
                Description = "怎樣的房配怎樣的人~怎樣的人配怎樣的Code~而你敢認清自己嗎?",
                Image = "https://kamiiliu.github.io/fake.png",
            };

            ViewData["seohelp"] = seohelp; 
            ViewData["url"] = $"https://social-plugins.line.me/lineit/share?url={HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value.ToString()}/Hallow";
            int userId = 0;
            string userName = "用戶";
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
                userName = _memberService.GetUserNameById(userId);
            };
            ViewData["userName"] = userName;
            return View();
		}
        public IActionResult Double11()
        {
            SeoHelpDto seohelp = new SeoHelpDto
            {
                WebAddress = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value.ToString()}/Double11",
                Title = "雙十一心理測驗:你最適合怎樣的旅行？",
                Description = "愉快的雙十一又到了，想好要去哪裡旅行了沒？如果還在為旅行地點傷腦筋，行程也依舊沒有確定下來，不妨透過下面的測試，隨「AirDnD」一起來看看，最適合你的旅行",
                Image = "https://kamiiliu.github.io/fake.png",
            };

            ViewData["seohelp"] = seohelp;
            ViewData["url"] = $"https://social-plugins.line.me/lineit/share?url={HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value.ToString()}/Double11";
            int userId = 0;
            string userName = "用戶";
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
                userName = _memberService.GetUserNameById(userId);
            };
            ViewData["userName"] = userName;
            return View();
        }
    }
}
