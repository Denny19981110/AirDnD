using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airdnd.Admin.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airdnd.Admin.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //在進入Action前過濾
    [ServiceFilter(typeof(CustomApiExceptionServiceFilter))]
    [ServiceFilter(typeof(DemoShopAdminAuthorize))]
    public abstract class BaseApiController : ControllerBase
    {
    }
}