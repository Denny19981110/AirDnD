using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airdnd.Admin.BaseModels;
using Airdnd.Core.Helper;
using Airdnd.Admin.WebApi;
using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Airdnd.Admin.Helpers;
using Airdnd.Admin.Service;
using Airdnd.Admin.Models;
using System.Security.Policy;
using System.Security.Claims;

namespace Airdnd.Admin.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly JwtHelper _jwt;
        private readonly IRepository<BlockToken> _blockTokenRepo;
        private readonly LoginService _loginService;

        public AuthController(JwtHelper jwt, IRepository<BlockToken> blockTokenRepo, LoginService loginService)
        {
            _jwt = jwt;
            _loginService = loginService;
            _blockTokenRepo = blockTokenRepo;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginDto request)
        {
            if (_loginService.IsUserValid(request))
            {
                return Ok(new BaseApiResponse(_jwt.GenerateToken(request.UserMail)));
            }
            return Ok(new BaseApiResponse() { IsSuccess = false, Code = Enums.ApiStatusEnum.NotFound });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserName()
        {
            return Ok(new BaseApiResponse(User.Identity.Name));

        }



        [HttpPost]
        public IActionResult Logout([FromBody] LogoutDTO request)
        {
            _blockTokenRepo.Add(new BlockToken
            {
                Token = request.Token,
                ExprieTime = DateTimeOffset.UtcNow.ToUniversalTime()
            });
            return Ok();
        }
    }
    public class LogoutDTO
    {
        public string Token { get; set; }
    }
}