using ErrorOr;
using Tech.Test.Payment.Application.Common.Interfaces.Services;
using Tech.Test.Payment.Application.Common.Security.CurrentUserProvider;
using Tech.Test.Payment.Application.Common.Security.Request;
using Tech.Test.Payment.Infrastructure.Security.PolicyEnforcer;

namespace Tech.Test.Payment.Infrastructure.Security
{
    public class AuthorizationService(
        IPolicyEnforcer _policyEnforcer,
        ICurrentUserProvider _currentUserProvider)
            : IAuthorizationService
    {
        public ErrorOr<Success> AuthorizeCurrentUser<T>(
            IAuthorizeableRequest<T> request,
            List<string> requiredRoles,
            List<string> requiredPermissions,
            List<string> requiredPolicies)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            if (requiredPermissions.Except(currentUser.Permissions).Any())
            {
                return Error.Unauthorized(description: "Usuário não tem permissões necessárias para essa ação");
            }

            if (requiredRoles.Except(currentUser.Roles).Any())
            {
                return Error.Unauthorized(description: "Usuário não tem papel necessário para essa ação");
            }

            foreach (var policy in requiredPolicies)
            {
                var authorizationAgainstPolicyResult = _policyEnforcer.Authorize(request, currentUser, policy);

                if (authorizationAgainstPolicyResult.IsError)
                {
                    return authorizationAgainstPolicyResult.Errors;
                }
            }

            return Result.Success;
        }
    }
}
