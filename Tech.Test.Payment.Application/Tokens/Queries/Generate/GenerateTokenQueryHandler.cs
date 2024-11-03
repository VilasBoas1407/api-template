using ErrorOr;

using MediatR;
using Tech.Test.Payment.Application.Common.Interfaces;

namespace Tech.Test.Payment.Application.Tokens.Queries.Generate
{
    public class GenerateTokenQueryHandler(
        IJwtTokenGenerator _jwtTokenGenerator) : IRequestHandler<GenerateTokenQuery, ErrorOr<GenerateTokenResult>>
    {
        public Task<ErrorOr<GenerateTokenResult>> Handle(GenerateTokenQuery query, CancellationToken cancellationToken)
        {
            var id = query.Id ?? Guid.NewGuid();

            var token = _jwtTokenGenerator.GenerateToken(id, 
                query.Cpf, 
                query.Name, 
                query.Email, 
                query.Phone,
                query.Permissions, 
                query.Roles);

            var authResult = new GenerateTokenResult(id, query.Cpf, query.Name, query.Email, query.Phone,token);

            return Task.FromResult(ErrorOrFactory.From(authResult));
        }
    }
}
