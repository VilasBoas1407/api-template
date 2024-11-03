using ErrorOr;

using MediatR;
using Tech.Test.Payment.Application.Common.Security;
using Tech.Test.Payment.Application.Common.Security.Permissions;
using Tech.Test.Payment.Application.Common.Security.Request;
using Tech.Test.Payment.Domain.Sales;

namespace Tech.Test.Payment.Application.Sales.Queries.GetSale;

[Authorize(Permissions = Permission.Sale.Get, Policies = Policy.SelfOrAdmin)]
public record GetSaleQuery(Guid SaleId): IAuthorizeableRequest<ErrorOr<Sale>>; 

