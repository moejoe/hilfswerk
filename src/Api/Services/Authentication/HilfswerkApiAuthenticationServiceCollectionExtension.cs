﻿using GraphQL.Authorization;
using Hilfswerk.Api.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HilfswerkApiAuthenticationServiceCollectionExtension
    {
        public static IServiceCollection AddSimpleTokenAuthentication(this IServiceCollection services, IConfiguration authConfiguration)
        {
            // lets use a singleton here and not reconfigure when the application.json changes.
            var tokenSettings = authConfiguration.Get<TokenServiceOptions>();
            services.Configure<TokenServiceOptions>(authConfiguration);

            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opt =>
               {
                   opt.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidAudience = tokenSettings.IssuerAudience,
                       ValidIssuer = tokenSettings.IssuerAudience,
                       IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenSettings.Secret))
                   };
               });
            services.TryAddSingleton(s =>
            {
                var authSettings = new AuthorizationSettings();
                authSettings.AddPolicy("Authenticated", _ => { _.RequireClaim(ClaimTypes.Name); });
                return authSettings;
            });
            return services;
        }
    }
}
