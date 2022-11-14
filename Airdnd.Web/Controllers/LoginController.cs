using Airdnd.Core.Helper;
using Airdnd.Web.Models;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using Airdnd.Core.enums;
using static Airdnd.Web.Models.GoogleSSO;
using static Airdnd.Web.Models.APIResult;
using System;

namespace Airdnd.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly MailService _mailService;
        private readonly LoginService _loginService;
        private readonly IConfiguration _config;
        public IConfigurationSection Channel_ID, Channel_Secret, CallbackURL;

        [TempData]
        public Guid State { get; set; }

        public LoginController(MailService mailService, LoginService loginService, IConfiguration config)
        {
            _mailService = mailService;
            _loginService = loginService;
            Channel_ID = config.GetSection("LINE-Login-Setting:Channel_ID");
            Channel_Secret = config.GetSection("LINE-Login-Setting:Channel_Secret");
            CallbackURL = config.GetSection("LINE-Login-Setting:CallbackURL");
            _config = config;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, string returnUrl)
        {
            var loginDto = new LoginDto()
            {
                Email = model.Email,
                Password = model.Password
            };

            var result = _loginService.Login(loginDto);

            if (result.Status == APIResult.APIStatus.Success)
            {
                if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
                return Redirect("/");
            }
            ViewData["ErrorMessage"] = result.Msg;
            ViewData["IsLoginFail"] = true;
            return View("Login", model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            ViewData["email"] = email;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            var dto = new ResetPasswordDto
            {
                Email = model.Email,
                Password = model.Password,
                CheckPassword = model.CheckPassword,
            };

            var result = _loginService.ResetPassword(dto);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["errorMessage"] = result.ErrorMessage;
                return View("ResetPassword");
            }
        }

        [Authorize]
        public IActionResult Logout()
        {
            _loginService.Logout();
            return Redirect("/");
        }

        /// <summary>
        /// Line登入回傳
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Callback()
        {
            var code = HttpContext.Request.Query["code"].ToString();
            //沒有抓到該用戶處理
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("Home", "Index");

            }
            //從code取得token
            var token = isRock.LineLoginV21.Utility.GetTokenFromCode(code,
                Channel_ID.Value, Channel_Secret.Value,
                CallbackURL.Value
                //CallbackURL
                );

            var JwtSecurityToken = new JwtSecurityToken(token.id_token);

            //取得用戶資訊
            var userInfo = isRock.LineLoginV21.Utility.GetUserProfile(token.access_token);
            var email = "";


            //SignIn
            var claims = new List<Claim>
                {
                    //use email or LINE user ID as login name
                    new Claim(ClaimTypes.Name, string.IsNullOrEmpty( email ) ? userInfo.userId:email ),
                    //use LINE displayName as FullName
                    new Claim("FullName",userInfo.displayName),
                    new Claim(ClaimTypes.Role, "nobody"),
                };
            //取Email
            if (JwtSecurityToken.Claims.ToList().Find(c => c.Type == "email") != null)
            {
                email = JwtSecurityToken.Claims.First(c => c.Type == "email").Value;
            }


            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };


            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity),
               authProperties);

            var dto = new SocialLoginInputDto()
            {
                Email = email,
                Name = userInfo.displayName,
                Photo = userInfo.pictureUrl,
                LoginType = LoginType.Line
            };

            //判斷帳戶是否存在
            if (!_loginService.IsAccountExist(dto.Email, LoginType.Line))
            {
                var result = _loginService.SocialCreateAccount(dto);
            }
            var logindto = new LoginDto
            {
                Email = dto.Email,
                LoginType = dto.LoginType
            };

            return RedirectToAction("Index", "Home", _loginService.Login(logindto));

        }

        public string GetGoogleLoginUrl()
        {
            var url = "https://accounts.google.com/o/oauth2/v2/auth?";
            url += "scope=email profile&";
            url += $"redirect_uri=https://{HttpContext.Request.Host}/Login/GoogleSSO&";
            url += "response_type=code&";
            url += $"client_id={_config["Google:client_id"]}&";
            url += $"state={State}";
            url += "approval_prompt=force&";

            return url;
        }

        public IActionResult GoogleSSO(string code, Guid state, string error_description)
        {
            if (State != state || string.IsNullOrEmpty(code))
            {
                ViewData["ErrorMessage"] = error_description;
                return RedirectToAction("Login");
            }
            //get token from code
            var token = Utility.GetTokenFromCode(code.ToString(),
                _config["Google:client_id"],
                _config["Google:client_secret"],
                $"https://{HttpContext.Request.Host}/Login/GoogleSSO");

            //取得使用者資訊
            var UserInfoResult = Utility.GetUserInfo(token.access_token);

            var dto = new SocialLoginInputDto()
            {
                Email = UserInfoResult.email,
                Name = UserInfoResult.name,
                Photo = UserInfoResult.picture,
                LoginType = LoginType.Google
            };

            if (!_loginService.IsAccountExist(dto.Email, LoginType.Google))
            {
                var result = _loginService.SocialCreateAccount(dto);
                if (result.Status == APIStatus.Fail)
                {
                    //註冊失敗
                }
            }
            var logindto = new LoginDto
            {
                Email = dto.Email,
                LoginType = dto.LoginType
            };

            return RedirectToAction("Index", "Home", _loginService.Login(logindto));
        }
    }
}
