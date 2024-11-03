using ErrorOr;
using Tech.Test.Payment.Application.Common.Security.Permissions;
using Tech.Test.Payment.Application.Common.Security.Request;
using Tech.Test.Payment.Application.Common.Security.Roles;
using Tech.Test.Payment.Application.Sales.Common;

namespace Tech.Test.Payment.Application.Sales.Commands.AddItemSale;

[Authorize(Permissions = Permission.Sale.AddItem, Roles = Role.Seller)]
public record AddItemSaleCommand(Guid SaleId, IList<ItemSaleDto> Items) : IAuthorizeableRequest<ErrorOr<Success>>;
