using GraphQL;
using GraphQL.Types;
using Hilfswerk.GraphApi;
using GraphQL.Server;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GraphQL.Authorization;
using Hilfswerk.EntityFramework;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GraphApiServiceCollectionExtension
    {
        public static IServiceCollection AddHilfswerkGraphApi(this IServiceCollection services)
        {
            services
                .AddScoped<HilfswerkSchema>()
                .AddGraphQL(options =>
                {
                    options.EnableMetrics = false;
                    options.ExposeExceptions = true;
                    options.UnhandledExceptionDelegate = ctx => { Console.WriteLine(ctx.OriginalException); };
                })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddSystemTextJson();
            //    .AddUserContextBuilder(httpContext => new AuthorizationContext { User = httpContext.User }); ; ;

            //services.AddHttpContextAccessor();


            //services.AddSingleton<HelferType>();
            //services.AddSingleton<KontaktType>();
            //services.AddSingleton<EinsatzType>();
            //services.AddSingleton<TaetigkeitEnumType>();
            //services.AddSingleton<HilfswerkMutation>();
            //services.AddSingleton<ISchema, HilfswerkSchema>();

            //services.AddSingleton<HilfswerkQuery>();


            //services.AddGraphQL(options =>
            //{
            //    options.EnableMetrics = true;
            //    options.ExposeExceptions = true;
            //})


            return services;
        }
    }
}
