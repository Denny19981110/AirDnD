using Airdnd.Admin.Models;
using Airdnd.Admin.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Airdnd.Admin.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionApiController : BaseApiController
    {
        private readonly TransactionService _transactionService;

        public TransactionApiController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet("GetOrder")]
        public IActionResult GetOrder()
        {
            var orders = _transactionService.GetTrans();
            return new JsonResult(orders);
        }
        [HttpPut("ChangeTranStatus")]
        public async Task<IActionResult> ChangeTranStatus(ChangeTranStatusDto order)
        {
            int id = order.OrderId;
            int result = await _transactionService.ChangeTranStatus(order.OrderId);
            if (result > 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("撥款失敗");
            }
        }
    }
}
