using ErrorOr;
using Tech.Test.Payment.Application.Common.Security.Permissions;
using Tech.Test.Payment.Application.Common.Security.Request;
using Tech.Test.Payment.Application.Common.Security.Roles;
using Tech.Test.Payment.Application.Sales.Common;
using Tech.Test.Payment.Domain.Sales;


namespace Tech.Test.Payment.Application.Sales.Commands.CreateSale;

[Authorize(Permissions = Permission.Sale.Create, Roles = Role.Seller)]
public record CreateSaleCommand(string CustomerName, string CustomerPhone, IList<ItemSaleDto> Items) : IAuthorizeableRequest<ErrorOr<Sale>>;

