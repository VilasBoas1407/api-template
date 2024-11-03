using ErrorOr;
using Tech.Test.Payment.Application.Common.Security;
using Tech.Test.Payment.Application.Common.Security.CurrentUserProvider;
using Tech.Test.Payment.Application.Common.Security.Request;
using Tech.Test.Payment.Application.Common.Security.Roles;

namespace Tech.Test.Payment.Infrastructure.Security.PolicyEnforcer;

public class PolicyEnforcer : IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(
        IAuthorizeableRequest<T> request,
        CurrentUser currentUser,
        string policy)
    {
        return policy switch
        {
            Policy.SelfOrAdmin => SelfOrAdminPolicy(request, currentUser),
            _ => Error.Unexpected(description: "Policy desconhecida."),
        };
    }

    private static ErrorOr<Success> SelfOrAdminPolicy<T>(IAuthorizeableRequest<T> request, CurrentUser currentUser) =>
        currentUser.Roles.Contains(Role.Admin)
            ? Result.Success
            : Error.Unauthorized(description: "Policy inválida.");
}