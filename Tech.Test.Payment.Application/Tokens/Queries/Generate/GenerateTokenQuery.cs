using ErrorOr;
using MediatR;

namespace Tech.Test.Payment.Application.Tokens.Queries.Generate;

public record GenerateTokenQuery(
    Guid? Id,
    string Cpf,
    string Name,
    string Email,
    List<string> Permissions,
    List<string> Roles) : IRequest<ErrorOr<GenerateTokenResult>>;