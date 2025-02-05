namespace Vilas.Template.Application.Common.Security.CurrentUserProvider;

public record CurrentUser(Guid Id,
    string Name,
    string Email,
    string Phone,
    string Cpf,
    IReadOnlyList<string> Permissions,
    IReadOnlyList<string> Roles);


