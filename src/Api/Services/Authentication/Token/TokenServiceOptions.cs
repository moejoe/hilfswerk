using System;

namespace Hilfswerk.Api.Services.Authentication
{
    public class TokenServiceOptions
    {
        public string Secret { get; set; }
        public string IssuerAudience { get; set; }
        public TimeSpan TokenLifeTime { get; set; } = TimeSpan.FromDays(7);
    }
}
