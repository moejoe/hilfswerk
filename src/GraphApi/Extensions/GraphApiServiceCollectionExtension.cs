using Hilfswerk.GraphApi;
using Hilfswerk.GraphApi.Queries;
using Hilfswerk.GraphApi.Mutations;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HilfswerkGraphSchemaServiceCollectionExtension
    {
        public static IServiceCollection AddHilfswerkGraphSchema(this IServiceCollection services, Action<GraphQLAuthorizationOptions> configureOptions)
        {
            services
                .Configure(configureOptions)
                .AddScoped<HilfswerkSchema>()
                .AddScoped<HilfswerkQuery>()
                .AddScoped<HilfswerkMutation>()
                .AddScoped<EinsatzType>()
                .AddScoped<EinsatzInputType>()
                .AddScoped<EinsatzEditInputType>()
                .AddScoped<CreateHelferInputType>()
                .AddScoped<EditHelferInputType>()
                .AddScoped<HelferType>()
                .AddScoped<KontaktInputType>()
                .AddScoped<KontaktType>()
                .AddScoped<TaetigkeitEnumType>();
            return services;
        }
    }
}
