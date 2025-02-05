using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vilas.Template.Application.Tokens.Queries.Generate;
using Vilas.Template.Contracts.Tokens;

namespace Vilas.Template.Api.Controllers
{
    [Route("api/tokens")]
    [AllowAnonymous]
    public class TokensController(ISender _mediator) : ApiController
    {
        /// <summary>
        /// Route to generate a JWT Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateToken(GenerateTokenRequest request)
        {
            var query = new GenerateTokenQuery(
                request.Id,
                request.Cpf,
                request.Name,
                request.Email,
                request.Phone,
                request.Permissions,
                request.Roles);

            var result = await _mediator.Send(query);

            return result.Match(
                generateTokenResult => Ok(ToDto(generateTokenResult)),
                Problem);
        }

        private static TokenResponse ToDto(GenerateTokenResult authResult)
        {
            return new TokenResponse(
                authResult.Id,
                authResult.Cpf,
                authResult.Name,
                authResult.Email,
                authResult.Token);
        }
    }
}
