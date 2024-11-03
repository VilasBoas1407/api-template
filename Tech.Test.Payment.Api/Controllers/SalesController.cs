using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tech.Test.Payment.Application.Sales.Commands.Create;
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
                    value: CreateToDto(sale)),
                Problem);
        }
    

        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetSaleById([FromRoute] Guid saleId)
        {
            var query = new GetSaleQuery(saleId);

            var result = await _mediator.Send(query);

            return result.Match(
                reminder => Ok(GetToDto(reminder)),
                Problem);
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


        private CreateSaleResponse CreateToDto(Sale sale) =>
            new(sale.Id,sale.CustomerName, sale.Items.Count);


        private GetSaleResponse GetToDto(Sale sale)
        {
            return new GetSaleResponse(sale.Id, sale.CustomerName, sale.CustomerPhone, sale.SellerId,
                sale.SellerName, sale.SellerCpf, sale.SellerEmail, sale.SellerPhone, sale.Status,
                sale.SaleDate, sale.Items.Select(x => new GetSaleItemResponse(x.Id, x.Name, x.Price, x.Quantity)).ToList());
        }
    }
}
