using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hilfswerk.Api.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly TokenServiceOptions _options;

        public TokenService(IOptionsSnapshot<TokenServiceOptions> optionsSnapshot)
        {
            _options = optionsSnapshot.Value ?? throw new ArgumentNullException(nameof(optionsSnapshot));
        }
        public Task<TokenResult> CreateTokenAsync(string subject)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, subject),
                    new Claim(JwtRegisteredClaimNames.Sub, subject)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _options.IssuerAudience,
                audience: _options.IssuerAudience,
                claims: claims,
                expires: DateTime.Now + _options.TokenLifeTime,
                signingCredentials: creds);

            return Task.FromResult(new TokenResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
