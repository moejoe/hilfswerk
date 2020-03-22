using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hilfswerk.Api.Services.Authentication
{
    public interface ITokenService
    {
        Task<TokenResult> CreateTokenAsync(string subject);
    }
}
