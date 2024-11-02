namespace Tech.Test.Payment.Contracts.Tokens;

public record TokenResponse(
    Guid Id,
    string Cpf,
    string Name,
    string Email,
    string Token);