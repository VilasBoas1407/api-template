namespace Vilas.Template.Contracts.Tokens;

public record GenerateTokenRequest(
    Guid? Id,
    string Cpf,
    string Name,
    string Email,
    string Phone,
    List<string> Permissions,
    List<string> Roles);
