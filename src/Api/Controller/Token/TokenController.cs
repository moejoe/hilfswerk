using System;
using System.Threading.Tasks;
using Hilfswerk.Api.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hilfswerk.Api.Controller.Token
{
    [Route("token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _tokenService.CreateTokenAsync(request.Name));
        }
    }

}