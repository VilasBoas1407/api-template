using ErrorOr;
using MediatR;
using Vilas.Template.Application.Tokens.Queries.Generate;

namespace Vilas.Template.Application.Tokens.Queries.Generate;

public record GenerateTokenQuery(
    Guid? Id,
    string Cpf,
    string Name,
    string Email,
    string Phone,
    List<string> Permissions,
    List<string> Roles) : IRequest<ErrorOr<GenerateTokenResult>>;