using ErrorOr;

using Tech.Test.Payment.Application.Common.Security.Request;
using Tech.Test.Payment.Application.Common.Security.Roles;
using Tech.Test.Payment.Application.Common.Security.Permissions;
using Tech.Test.Payment.Domain.Sells;

namespace Tech.Test.Payment.Application.Sales.Commands.UpdateSaleStatus;

[Authorize(Permissions = Permission.Sale.UpdateStatus, Roles = Role.Seller)]
public record class UpdateSaleStatusCommand(Guid SaleId, SaleStatus NewStatus) : IAuthorizeableRequest<ErrorOr<Success>>;
