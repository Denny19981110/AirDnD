using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Airdnd.Admin.Filters
{
    public class DemoShopAdminAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly IRepository<BlockToken> _blockToken;

        public DemoShopAdminAuthorize(IRepository<BlockToken> blockToken)
        {
            _blockToken = blockToken;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Action有掛AllowAnonymous時候return
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            //如果是null就回傳false
            if (context.HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                //登出後Token失效Filter(不能再登入，要取得新的)
                //var authorization = context.HttpContext.Request.Headers["Authorization"];
                //var token = authorization.ToString().Split("bearer ").Last();

                //如同37行，但別人已經封裝好
                var authorization = context.HttpContext.Request.Headers[HeaderNames.Authorization];
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    var a = !_blockToken.Any(x => x.Token.ToLower() == headerValue.Parameter.ToLower());
                    if (!_blockToken.Any(x => x.Token.ToLower() == headerValue.Parameter.ToLower()))
                    {
                        return;
                    }

                }
            }

            //context.Result = new ForbidResult();
            context.Result = new UnauthorizedResult();
        }
    }
}