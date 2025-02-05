using ErrorOr;
using Vilas.Template.Application.Common.Security;
using Vilas.Template.Application.Common.Security.CurrentUserProvider;
using Vilas.Template.Application.Common.Security.Request;
using Vilas.Template.Application.Common.Security.Roles;

namespace Vilas.Template.Infrastructure.Security.PolicyEnforcer;

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