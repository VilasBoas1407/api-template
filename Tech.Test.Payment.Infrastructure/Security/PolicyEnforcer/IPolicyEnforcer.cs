using ErrorOr;

using Tech.Test.Payment.Application.Common.Security.CurrentUserProvider;
using Tech.Test.Payment.Application.Common.Security.Request;

namespace Tech.Test.Payment.Infrastructure.Security.PolicyEnforcer;

public interface IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(
    IAuthorizeableRequest<T> request,
    CurrentUser currentUser,
    string policy);
}