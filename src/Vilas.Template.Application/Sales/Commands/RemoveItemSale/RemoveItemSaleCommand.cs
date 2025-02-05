using ErrorOr;
using Vilas.Template.Application.Common.Security.Permissions;
using Vilas.Template.Application.Common.Security.Request;
using Vilas.Template.Application.Common.Security.Roles;

namespace Vilas.Template.Application.Sales.Commands.RemoveItemSale;

[Authorize(Permissions = Permission.Sale.RemoveItem, Roles = Role.Seller)]
public record class RemoveItemSaleCommand(Guid SaleId,IList<Guid> IdsItemsSales) : IAuthorizeableRequest<ErrorOr<Success>>;

