using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vilas.Template.Application.Common.Interfaces;
using Vilas.Template.Application.Common.Security.TokenGenerator;
using Vilas.Template.Infrastructure.Security.Common;

namespace Vilas.Template.Infrastructure.Security.TokenGenerator;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
{

    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public string GenerateToken(Guid id, string cpf, string name, string email, string phone, List<string> permissions, List<string> roles)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtClaimNames.Id, id.ToString()),
            new(JwtClaimNames.Name, name),
            new(JwtClaimNames.PhoneNumber, phone),
            new(JwtClaimNames.Cpf, cpf),
            new(JwtClaimNames.Email, email),
        };

        roles.ForEach(role => claims.Add(new(JwtClaimNames.Role, role)));
        permissions.ForEach(permission => claims.Add(new(JwtClaimNames.Permissions, permission)));

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
