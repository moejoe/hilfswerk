using GraphQL.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class HilfswerkGraphQLServerServiceCollectionExtension
    {
        public static IServiceCollection AddHilfswerkGraphApi(this IServiceCollection services)
        {
            services
                .AddHilfswerkGraphSchema();
            services
                .AddGraphQL(options =>
                    {
                        options.EnableMetrics = false;
                        options.ExposeExceptions = true;
                        options.UnhandledExceptionDelegate = ctx => { Console.WriteLine(ctx.OriginalException); };
                    })
                .AddGraphQLAuthorization(d =>
                {
                    d.AddPolicy("DefaultPolicy", new AuthorizationPolicyBuilder(new[] { JwtBearerDefaults.AuthenticationScheme })
                    .RequireAuthenticatedUser()
                    .Build());
                })
                .AddSystemTextJson();
            return services;
            // Authorization ? 
            //.AddUserContextBuilder(httpContext => new AuthorizationContext { User = httpContext.User }); ; ;
            //services.AddHttpContextAccessor();
        }
    }
}
