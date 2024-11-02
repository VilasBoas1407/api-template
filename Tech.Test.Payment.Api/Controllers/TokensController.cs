using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using Tech.Test.Payment.Contracts.Tokens;

namespace Tech.Test.Payment.Api.Controllers
{
    [Route("/api/tokens")]
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
            var query = new GenerateTokenQuery()
            return Ok(request);
        }


        [HttpGet]
        public async Task <IActionResult> GetToken()
        {
            return Ok("Hello world");
        }
    }
}
