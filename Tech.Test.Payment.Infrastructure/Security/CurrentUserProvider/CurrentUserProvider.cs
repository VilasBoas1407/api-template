using Microsoft.AspNetCore.Http;
using Tech.Test.Payment.Infrastructure.Security.Common;
using Tech.Test.Payment.Application.Common.Security.CurrentUserProvider;

namespace Tech.Test.Payment.Infrastructure.Security.CurrentUserProvider;

public class CurrentUserProvider :  ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CurrentUser GetCurrentUser()
    {
        var userClaims = _httpContextAccessor.HttpContext.User;

        var id = userClaims.FindFirst(JwtClaimNames.Id)?.Value;
        var email = userClaims.FindFirst(JwtClaimNames.Email)?.Value;
        var cpf = userClaims.FindFirst(JwtClaimNames.Cpf)?.Value;
        var name = userClaims.FindFirst(JwtClaimNames.Name)?.Value;
        var phone = userClaims.FindFirst(JwtClaimNames.PhoneNumber)?.Value;

        return new CurrentUser(Guid.Parse(id),name,email,phone,cpf);

    }

}
