using ErrorOr;
using Vilas.Template.Application.Common.Security.Request;
using Vilas.Template.Domain.Sells;
using Vilas.Template.Application.Common.Security.Roles;
using Vilas.Template.Application.Common.Security.Permissions;

namespace Vilas.Template.Application.Sales.Commands.UpdateSaleStatus;

[Authorize(Permissions = Permission.Sale.UpdateStatus, Roles = Role.Seller)]
public record class UpdateSaleStatusCommand(Guid SaleId, SaleStatus NewStatus) : IAuthorizeableRequest<ErrorOr<Success>>;
