using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace Airdnd.Web.Services
{
    public class LineService : PageModel
    {
        public IConfigurationSection Channel_ID, Channel_Secret, CallbackURL;

        public LineService(IConfiguration configuration)
        {
            Channel_ID = configuration.GetSection("LINE-Login-Setting:Channel_ID");
            Channel_Secret = configuration.GetSection("LINE-Login-Setting:Channel_Secret");
            CallbackURL = configuration.GetSection("LINE-Login-Setting:CallbackURL");
        }

        public string GetMail()
        {
            var code = HttpContext.Request.Query["code"].ToString();
            if (string.IsNullOrEmpty(code))
            {
                using(var sw = new System.IO.StreamWriter(HttpContext.Response.Body, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine("發生錯誤");
                    sw.Flush();
                    return sw.ToString();
                }
            }

            var token = isRock.LineLoginV21.Utility.GetTokenFromCode(code, Channel_ID.Value, Channel_Secret.Value, CallbackURL.Value);

            //用access_token 取得用戶資料
            var user = isRock.LineLoginV21.Utility.GetUserProfile(token.access_token);
            //利用id_token取得Claim資料
            var JwtSecurityToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(token.id_token);

            var email = "";
            if (JwtSecurityToken.Claims.ToList().Find(c => c.Type == "email") != null)
                email = JwtSecurityToken.Claims.First(c => c.Type == "email").Value;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, string.IsNullOrEmpty( email ) ? user.userId:email ),
                //use LINE displayName as FullName
                new Claim("FullName",user.displayName),
                new Claim(ClaimTypes.Role, "nobody"),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };
            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity),
               authProperties);
            var text = $"index?token={System.Web.HttpUtility.UrlEncode(token.access_token)}&email={email}";
            return text;
        }

    }
}
