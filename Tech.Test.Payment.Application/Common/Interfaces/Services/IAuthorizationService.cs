using ErrorOr;
using Tech.Test.Payment.Application.Common.Security.Request;

namespace Tech.Test.Payment.Application.Common.Interfaces.Services;

public interface IAuthorizationService
{
    ErrorOr<Success> AuthorizeCurrentUser<T>(
        IAuthorizeableRequest<T> request,
        List<string> requiredRoles,
        List<string> requiredPermissions,
        List<string> requiredPolicies);
}