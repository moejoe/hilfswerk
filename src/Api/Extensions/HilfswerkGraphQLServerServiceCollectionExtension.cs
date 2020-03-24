using GraphQL.Server;
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
                .AddSystemTextJson();
            return services;
            // Authorization ? 
            //.AddUserContextBuilder(httpContext => new AuthorizationContext { User = httpContext.User }); ; ;
            //services.AddHttpContextAccessor();
        }
    }
}
