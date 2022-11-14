using Airdnd.Core.enums;
using Airdnd.Web.Controllers;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Services;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airdnd.Web.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberApiController : ControllerBase
    {
        private readonly MemberService _memberService;
        private readonly LoginService _loginService;

        public MemberApiController(MemberService memberService, LoginService loginService)
        {
            _memberService = memberService;
            _loginService = loginService;
        }
        

        [HttpGet]
        public IActionResult GetMemberData()
        {
            var userId = _loginService.GetCurrentUserId();
            return new JsonResult(_memberService.GetUserDataById(userId));
        }

        [HttpPost]
        public IActionResult EditMemberData([FromBody] MemberData model)
        {            
            var dto = new MemberDataDto()
            {
                Name = model.Name,
                Email = model.Email,
                Gender = (GenderType)model.Gender,
                Phone = model.Phone,
                Address = model.Address
            };
            var userId = _loginService.GetCurrentUserId() ;
            return new JsonResult(_memberService.EditMemberData(dto, userId));
        }

        [HttpPost]
        public IActionResult EditPassword([FromBody] ResetPasswordDto model)
        {
            var userId = _loginService.GetCurrentUserId();
            return new JsonResult(_memberService.ModifyPassword(model, userId));
        }
        [HttpGet]
        public IActionResult GetPasswordModifyDate()
        {
            var userId = _loginService.GetCurrentUserId();
            return new JsonResult(_memberService.GetPasswordModifyDate(userId));
        }
    }
}
