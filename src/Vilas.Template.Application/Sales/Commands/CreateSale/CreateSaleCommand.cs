using ErrorOr;
using Vilas.Template.Application.Common.Security.Permissions;
using Vilas.Template.Application.Common.Security.Request;
using Vilas.Template.Application.Common.Security.Roles;
using Vilas.Template.Application.Sales.Common;
using Vilas.Template.Domain.Sales;

namespace Vilas.Template.Application.Sales.Commands.CreateSale;

[Authorize(Permissions = Permission.Sale.Create, Roles = Role.Seller)]
public record CreateSaleCommand(string CustomerName, string CustomerPhone, IList<ItemSaleDto> Items) : IAuthorizeableRequest<ErrorOr<Sale>>;

