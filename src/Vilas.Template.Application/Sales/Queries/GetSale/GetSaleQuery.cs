using ErrorOr;
using Vilas.Template.Application.Common.Security.Request;
using Vilas.Template.Application.Common.Security.Roles;
using Vilas.Template.Domain.Sales;
using Vilas.Template.Application.Common.Security.Permissions;
namespace Vilas.Template.Application.Sales.Queries.GetSale;

[Authorize(Permissions = Permission.Sale.Get, Roles = Role.Seller)]
public record GetSaleQuery(Guid SaleId): IAuthorizeableRequest<ErrorOr<Sale>>; 

