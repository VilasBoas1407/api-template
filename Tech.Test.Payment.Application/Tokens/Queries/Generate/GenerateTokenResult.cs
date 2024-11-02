namespace Tech.Test.Payment.Application.Tokens.Queries.Generate;

public record GenerateTokenResult(
    Guid Id,
    string Cpf,
    string Name,
    string Email,
    string Token);