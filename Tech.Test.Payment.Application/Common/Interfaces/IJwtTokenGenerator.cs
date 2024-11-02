namespace Tech.Test.Payment.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(
    Guid id,
    string cpf,
    string name,
    string email,
    List<string> permissions,
    List<string> roles);
}
