namespace Tech.Test.Payment.Contracts.Tokens;

public record GenerateTokenRequest(
    Guid? Id,
    string Cpf,
    string Name,
    string Email,
    List<string> Permissions,
    List<string> Roles);
