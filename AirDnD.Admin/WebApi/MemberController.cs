using Airdnd.Admin.Models;
using Airdnd.Admin.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Airdnd.Admin.WebApi
{
    [Route("api/[controller]")]
    [ApiController]

    public class MemberController : BaseApiController
    {
        private MemberService _memberService;
        public MemberController(MemberService memberService)
        {
            _memberService=memberService;
        }
        [HttpGet("GetMemberData")]
        public IActionResult GetMemberData()
        {
            var member = _memberService.GetAllMember();
            return new JsonResult(member);
        }


    }
}
