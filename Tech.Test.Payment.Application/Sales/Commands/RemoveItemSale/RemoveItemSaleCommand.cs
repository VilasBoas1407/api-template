using ErrorOr;
using Tech.Test.Payment.Application.Common.Security.Permissions;
using Tech.Test.Payment.Application.Common.Security.Request;
using Tech.Test.Payment.Application.Common.Security.Roles;

namespace Tech.Test.Payment.Application.Sales.Commands.RemoveItemSale;

[Authorize(Permissions = Permission.Sale.RemoveItem, Roles = Role.Seller)]
public record class RemoveItemSaleCommand(Guid SaleId,IList<Guid> IdsItemsSales) : IAuthorizeableRequest<ErrorOr<Success>>;

