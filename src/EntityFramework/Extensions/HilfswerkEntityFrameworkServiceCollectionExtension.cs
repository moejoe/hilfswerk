using EntityFramework.Reporting;
using Hilfswerk.Core.Reporting;
using Hilfswerk.Core.Stores;
using Hilfswerk.EntityFramework.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HilfswerkEntityFrameworkServiceCollectionExtension
    {
        public static IServiceCollection AddHilfswerkEntityFramework(this IServiceCollection services)
        {
            return services
                .AddScoped<IHelferStore, EfHelferStore>()
                .AddScoped<IReportGenerator, ReportGenerator>();
        }
    }
}
