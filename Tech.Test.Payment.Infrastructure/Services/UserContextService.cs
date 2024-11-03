using Microsoft.AspNetCore.Http;
using Tech.Test.Payment.Application.Common.Dtos;
using Tech.Test.Payment.Application.Common.Interfaces.Services;
using Tech.Test.Payment.Infrastructure.Security.Common;

namespace Tech.Test.Payment.Infrastructure.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UserContextDto GetCurrentUser()
    {
        var userClaims = _httpContextAccessor.HttpContext.User;

        var userIdClaim = userClaims.FindFirst(JwtClaimNames.Id)?.Value;
        var emailClaim = userClaims.FindFirst(JwtClaimNames.Email)?.Value; 
        var cpfClaim = userClaims.FindFirst(JwtClaimNames.Email)?.Value;
        var userNameClaim = userClaims.FindFirst(JwtClaimNames.Name)?.Value;
        var phoneClaim = userClaims.FindFirst(JwtClaimNames.PhoneNumber)?.Value;

        return new UserContextDto
        {
            Id = Guid.Parse(userIdClaim),
            Name = userNameClaim,
            Email = emailClaim,
            Phone = phoneClaim,
            Cpf = cpfClaim
        };
    }

}
