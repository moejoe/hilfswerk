using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Hilfswerk.GraphApi;
using GraphQL.Server;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GraphQL.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GraphApiServiceCollectionExtension
    {
        public static IServiceCollection AddHilfswerkGraphApi(this IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.TryAddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddHttpContextAccessor();


            services.AddSingleton<HelferType>();
            services.AddSingleton<KontaktType>();
            services.AddSingleton<EinsatzType>();
            services.AddSingleton<TaetigkeitEnumType>();
            services.AddSingleton<HilfswerkMutation>();
            services.AddSingleton<ISchema, HilfswerkSchema>();

            services.AddSingleton(new HilfswerkQuery(TestData.Helfer));


            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            })
            .AddUserContextBuilder(httpContext => new AuthorizationContext { User = httpContext.User }); ;

            return services;
        }
    }
}
