using ErrorOr;
using Tech.Test.Payment.Application.Common.Security;
using Tech.Test.Payment.Application.Common.Security.Permissions;
using Tech.Test.Payment.Application.Common.Security.Request;
using Tech.Test.Payment.Domain.Sales;


namespace Tech.Test.Payment.Application.Sales.Commands.Create;

[Authorize(Permissions = Permission.Sale.Create, Policies = Policy.SelfOrAdmin)]
public record CreateSaleCommand(string CustomerName, string CustomerPhone, IList<SaleItemDto> Items) : IAuthorizeableRequest<ErrorOr<Sale>>;

public class SaleItemDto()
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}