using GraphQL.Server;
using Hilfswerk.GraphApi.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class HilfswerkGraphQLServerServiceCollectionExtension
    {
        public static IServiceCollection AddHilfswerkGraphApi(this IServiceCollection services, Action<GraphQLAuthorizationOptions> configureOptions)
        {
            services
                .AddHilfswerkGraphSchema(configureOptions);
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
        }
    }
}
