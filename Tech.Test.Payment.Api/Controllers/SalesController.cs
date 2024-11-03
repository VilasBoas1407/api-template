using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tech.Test.Payment.Application.Sales.Commands.AddItemSale;
using Tech.Test.Payment.Application.Sales.Commands.CreateSale;
using Tech.Test.Payment.Application.Sales.Common;
using Tech.Test.Payment.Application.Sales.Queries.GetSale;
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
                    new ItemSaleDto(x.ItemName, x.ItemPrice, x.ItemQuantity))
                .ToList();

            var command = new CreateSaleCommand(request.CustomerName, request.CustomerPhone, items);

            var result = await _mediator.Send(command);

            return result.Match(
                sale => CreatedAtAction(
                    actionName: nameof(GetSaleById),
                    routeValues: new { SaleId = sale },
                    value: CreateToDto(sale)),
                Problem);
        }

        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetSaleById([FromRoute] Guid saleId)
        {
            var query = new GetSaleQuery(saleId);

            var result = await _mediator.Send(query);

            return result.Match(
                sale => Ok(GetToDto(sale)),
                Problem);
        }

        [HttpPatch("{saleId}/add/itens")]
        public async Task<IActionResult> PatchItens([FromRoute] Guid saleId, [FromBody] IList<AddItemSaleRequest> request)
        {
            var items = request.
                Select(x =>
                    new ItemSaleDto(x.ItemName, x.ItemPrice, x.ItemQuantity))
                .ToList();

            var command = new AddItemSaleCommand(saleId, items);

            var result = await _mediator.Send(command);

            return result.Match(
                sale => NoContent(),
                Problem);
        }

        [HttpPut("{saleId}/status/{newStatus}")]
        public async Task<IActionResult> PutStatus([FromRoute] Guid saleId, string newStatus)
        {
            return Ok();
        }


        private CreateSaleResponse CreateToDto(Sale sale) =>
            new(sale.Id, sale.CustomerName, sale.Items.Count);


        private GetSaleResponse GetToDto(Sale sale)
        {
            return new GetSaleResponse(sale.Id, sale.CustomerName, sale.CustomerPhone, sale.SellerId,
                sale.SellerName, sale.SellerCpf, sale.SellerEmail, sale.SellerPhone, sale.Status,
                sale.SaleDate, sale.Items.Select(x => new GetSaleItemResponse(x.Id, x.Name, x.Price, x.Quantity)).ToList());
        }
    }
}
