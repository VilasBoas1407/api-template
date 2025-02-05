namespace Vilas.Template.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(
    Guid id,
    string cpf,
    string name,
    string email,
    string phone,
    List<string> permissions,
    List<string> roles);
}
