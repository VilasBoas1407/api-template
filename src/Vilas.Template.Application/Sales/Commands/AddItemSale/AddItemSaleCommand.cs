using ErrorOr;
using Vilas.Template.Application.Sales.Common;
using Vilas.Template.Application.Common.Security.Request;
using Vilas.Template.Application.Common.Security.Roles;
using Vilas.Template.Application.Common.Security.Permissions;

namespace Vilas.Template.Application.Sales.Commands.AddItemSale;

[Authorize(Permissions = Permission.Sale.AddItem, Roles = Role.Seller)]
public record AddItemSaleCommand(Guid SaleId, IList<ItemSaleDto> Items) : IAuthorizeableRequest<ErrorOr<Success>>;
