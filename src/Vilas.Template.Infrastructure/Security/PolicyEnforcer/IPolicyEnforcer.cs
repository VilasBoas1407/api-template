using ErrorOr;
using Vilas.Template.Application.Common.Security.CurrentUserProvider;
using Vilas.Template.Application.Common.Security.Request;

namespace Vilas.Template.Infrastructure.Security.PolicyEnforcer;

public interface IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(
    IAuthorizeableRequest<T> request,
    CurrentUser currentUser,
    string policy);
}