using Hilfswerk.Api.Services.Authentication.UserInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hilfswerk.Api.Controller.UserInfo
{
    [ApiController]
    [Route("oauth2/userinfo")]
    [Authorize]
    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            return Ok(User?.ToUserInfo());
        }
    }
}
