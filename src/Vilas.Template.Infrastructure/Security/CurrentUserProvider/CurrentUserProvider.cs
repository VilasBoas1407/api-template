using Microsoft.AspNetCore.Http;
using Vilas.Template.Application.Common.Security.CurrentUserProvider;
using Vilas.Template.Infrastructure.Security.Common;

namespace Vilas.Template.Infrastructure.Security.CurrentUserProvider;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        var id = GetSingleClaimValue(JwtClaimNames.Id);
        var email = GetSingleClaimValue(JwtClaimNames.Email);
        var cpf = GetSingleClaimValue(JwtClaimNames.Cpf);
        var name = GetSingleClaimValue(JwtClaimNames.Name);
        var phone = GetSingleClaimValue(JwtClaimNames.PhoneNumber);
        var permissions = GetClaimValues(JwtClaimNames.Permissions);
        var roles = GetClaimValues(JwtClaimNames.Role);

        return new CurrentUser(Guid.Parse(id), name, email, phone, cpf, permissions, roles);

    }

    private List<string> GetClaimValues(string claimType) =>
    _httpContextAccessor.HttpContext!.User.Claims
        .Where(claim => claim.Type == claimType)
        .Select(claim => claim.Value)
        .ToList();

    private string GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value ?? "";
}
