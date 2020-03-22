using System.Security.Claims;

namespace Hilfswerk.Api.Services.Authentication.UserInfo
{
    internal static class HilfswerkApiUserInfoUserExtension
    {
        public static UserInfo ToUserInfo(this ClaimsPrincipal user)
        {
            return new UserInfo
            {
                Name = user.Identity.Name
            };
        }
    }
}
