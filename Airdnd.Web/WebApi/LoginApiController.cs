using Airdnd.Core.Entities;
using Airdnd.Core.enums;
using Airdnd.Web.Models;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using static System.Net.WebRequestMethods;
using isRock.LIFF;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Airdnd.Web.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase 
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _config;
        public IConfigurationSection Channel_ID, Channel_Secret, CallbackURL;
        public LoginApiController(LoginService loginService, IConfiguration config)
        {
            _loginService = loginService;
            _config = config;
            Channel_ID = config.GetSection("LINE-Login-Setting:Channel_ID");
            Channel_Secret = config.GetSection("LINE-Login-Setting:Channel_Secret");
            CallbackURL = config.GetSection("Line-Login-Setting:CallbackURL");
            //CallbackURL = config.GetSection("LINE-Login-Setting:CallbackURL");
        }

        [HttpPost("CheckEmail/{email}")]
        public IActionResult CheckEmail(string email)
        {
            //var dto = new LoginEmailDto { Email = email };
            return new JsonResult(_loginService.IsAccountExist(email, 0));
        }

        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] SignupViewModel model)
        {
            var dto = new SignupDto
            {
                Name = model.Name,
                Birthday = model.Birthday,
                Email = model.Email,
                Password = model.Password
            };
            return new JsonResult(_loginService.Signup(dto));
        }
        [HttpPost("AcceptPromise")]
        public IActionResult AcceptPromise([FromBody] SignupViewModel model)
        {

            return new JsonResult(_loginService.AcceptPromise(model.Email, model.Name));
        }
        
        [HttpPost("DeleteAccount/{email}")]
        public IActionResult DeleteAccount(string email)
        {
            return new JsonResult(_loginService.DelectAccount(email));
        }

        [HttpPost("SignupPhone")]
        public IActionResult SignupPhone([FromBody] SignupViewModel model)
        {
            var dto = new SignupDto
            {
                Email = model.Email,
                Phone = model.Phone,
                AvatarUrl = model.AvatarUrl
            };
            return new JsonResult(_loginService.SignupPhone(dto));
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginViewModel request)
        {
            var loginDto = new LoginDto()
            {
                Email = request.Email,
                Password = request.Password
            };
            
            return new JsonResult(_loginService.Login(loginDto));
        }

        [HttpPost("ForgetPassword/{email}")]
        public IActionResult ForgetPassword(string email)
        {
            return new JsonResult(_loginService.ForgetPassword(email));
        }

        [HttpPost("FacebookLogin")]
        public IActionResult FacebookLogin([FromBody] FbLoginDataModel request)
        {
            var inputDto = new SocialLoginInputDto()
            {
                Email = request.Email,
                Name = request.Name,
                LoginType = LoginType.Facebook
            };
            if (!_loginService.IsAccountExist(request.Email, LoginType.Facebook))
            {
                //先註冊
                _loginService.SocialCreateAccount(inputDto);
            }
            //再登入
            var loginDto = new LoginDto()
            {
                Email = inputDto.Email,
                LoginType = inputDto.LoginType
            };
            
            return new JsonResult(_loginService.Login(loginDto));
        }

        //[HttpGet("GetGoogleLoginUrl")]
        //public IActionResult GetGoogleLoginUrl()
        //{
        //    var url = $"https://accounts.google.com/o/oauth2/auth?" +
        //        $"scope={HttpUtility.UrlEncode("profile email")}" +
        //        $"&response_type=code" +
        //        $"&state={State}" +
        //        $"&redirect_uri={RedirectUrl}/Member/GoogleLogin" +
        //        $"&client_id={_config["Google:ClientId"]}";
        //}




        //[FromBody] GoogleLoginModel request
        //[HttpPost("GoogleLogin")]
        //public IActionResult GoogleLogin()
        //{
        //    string formCredential = Request.Form["credential"]; //回傳憑證
        //    string formToken = Request.Form["g_csrf_token"]; //回傳令牌
        //    string cookiesToken = Request.Cookies["g_csrf_token"]; //Cookie 令牌

        //    // 驗證 Google Token
        //    GoogleJsonWebSignature.Payload payload = VerifyGoogleToken(formCredential, formToken, cookiesToken).Result;
        //    if (payload == null)
        //    {
        //        // 驗證失敗
        //        //ViewData["Msg"] = "驗證 Google 授權失敗";
        //        return new JsonResult("驗證 Google 授權失敗");
        //    }
        //    else
        //    {
        //        //驗證成功，取使用者資訊內容
        //        //ViewData["Msg"] = "驗證 Google 授權成功" + "<br>";
        //        //ViewData["Msg"] += "Email:" + payload.Email + "<br>";
        //        //ViewData["Msg"] += "Name:" + payload.Name + "<br>";
        //        //ViewData["Msg"] += "Picture:" + payload.Picture;
        //        var dto = new SocialLoginInputDto()
        //        {
        //            Email = payload.Email,
        //            Name = payload.Name,
        //            Photo = payload.Picture,
        //            LoginType = LoginType.Google
        //        };
        //        if(!_loginService.IsAccountExist(dto.Email, LoginType.Google))
        //        {
        //            //先註冊
        //            _loginService.SocialCreateAccount(dto);
        //        }
        //        var logindto = new LoginDto
        //        {
        //            Email = dto.Email,
        //            LoginType = dto.LoginType
        //        };
        //        return new JsonResult(_loginService.Login(logindto));

        //    }

        //}
        ///// <summary>
        ///// 驗證 Google Token
        ///// </summary>
        ///// <param name="formCredential"></param>
        ///// <param name="formToken"></param>
        ///// <param name="cookiesToken"></param>
        ///// <returns></returns>
        //public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string? formCredential, string? formToken, string? cookiesToken)
        //{
        //    // 檢查空值
        //    if (formCredential == null || formToken == null && cookiesToken == null)
        //    {
        //        return null;
        //    }

        //    GoogleJsonWebSignature.Payload? payload;
        //    try
        //    {
        //        // 驗證 token
        //        if (formToken != cookiesToken)
        //        {
        //            return null;
        //        }

        //        // 驗證憑證
        //        //IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        //        //string GoogleApiClientId = Config.GetSection("GoogleApiClientId").Value;
        //        string GoogleApiClientId = _config["Google:client_id"];

        //        var settings = new GoogleJsonWebSignature.ValidationSettings()
        //        {
        //            Audience = new List<string>() { GoogleApiClientId }
        //        };
        //        payload = await GoogleJsonWebSignature.ValidateAsync(formCredential, settings);
        //        if (!payload.Issuer.Equals("accounts.google.com") && !payload.Issuer.Equals("https://accounts.google.com"))
        //        {
        //            return null;
        //        }
        //        if (payload.ExpirationTimeSeconds == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            DateTime now = DateTime.Now.ToUniversalTime();
        //            DateTime expiration = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).DateTime;
        //            if (now > expiration)
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    return payload;
        //}
    }
}
