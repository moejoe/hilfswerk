using Hilfswerk.GraphApi;
using Hilfswerk.GraphApi.Queries;
using Hilfswerk.GraphApi.Mutations;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HilfswerkGraphSchemaServiceCollectionExtension
    {
        public static IServiceCollection AddHilfswerkGraphSchema(this IServiceCollection services)
        {
            services
                .AddScoped<HilfswerkSchema>()
                .AddScoped<HilfswerkQuery>()
                .AddScoped<HilfswerkMutation>()
                .AddScoped<EinsatzInputType>()
                .AddScoped<HelferInputType>()
                .AddScoped<HelferType>()
                .AddScoped<KontaktInputType>()
                .AddScoped<KontaktType>()
                .AddScoped<TaetigkeitEnumType>();
            return services;
        }
    }
}
