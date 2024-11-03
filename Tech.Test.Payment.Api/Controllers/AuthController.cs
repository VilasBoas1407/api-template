using Microsoft.AspNetCore.Mvc;

namespace Tech.Test.Payment.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}
