using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tech.Test.Payment.Application.Sales.Commands.Create;
using Tech.Test.Payment.Contracts.Sales;
using Tech.Test.Payment.Domain.Sales;

namespace Tech.Test.Payment.Api.Controllers
{
    [Route("api/sales")]
    public class SalesController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleRequest request)
        {
            var items = request.Items.
                Select(x => 
                    new SaleItemDto { Name = x.ItemName, 
                        Price = x.ItemPrice, 
                        Quantity = x.ItemQuantity })
                .ToList();

            var command = new CreateSaleCommand(request.CustomerName, request.CustomerPhone, items);

            var result = await _mediator.Send(command);

            return result.Match(
                sale => CreatedAtAction(
                    actionName: nameof(GetSaleById),
                    routeValues: new { SaleId = sale },
                    value: ToDto(sale)),
                Problem);
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


        private CreateSaleResponse ToDto(Sale sale) =>
            new(sale.Id,sale.CustomerName, sale.Items.Count);
    }
}
