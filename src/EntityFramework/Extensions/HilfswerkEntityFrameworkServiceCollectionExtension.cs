using Hilfswerk.Core.Stores;
using Hilfswerk.EntityFramework.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HilfswerkEntityFrameworkServiceCollectionExtension
    {
        public static IServiceCollection AddHilfswerkEntityFrameworkStores(this IServiceCollection services)
        {
            return services.
                AddScoped<IHelferStore, EfHelferStore>();

        }
    }
}
