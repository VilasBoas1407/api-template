using Microsoft.AspNetCore.Mvc;

namespace Tech.Test.Payment.Api.Controllers
{
    [Route("api/sales")]
    public class SalesController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateSale()
        {
            return Ok();
        }

        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetSaleById([FromRoute] Guid saleId)
        {
            return Ok();
        }

        [HttpPatch("{saleId}/itens")]
        public async Task<IActionResult> PatchItens([FromRoute] Guid saleId)
        {
            return Ok();
        }

        [HttpPut("{saleId}/status/{newStatus}")]
        public async Task<IActionResult> PutStatus([FromRoute] Guid saleId, string newStatus)
        {
            return Ok();
        }
    }
}
