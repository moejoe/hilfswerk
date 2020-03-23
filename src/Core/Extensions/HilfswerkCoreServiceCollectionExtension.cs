namespace Microsoft.Extensions.DependencyInjection
{
    public static class HilfswerkCoreServiceCollectionExtension
    {
        public static IServiceCollection AddHilfswerkCore(this IServiceCollection services)
        {
            return services
                .AddAuthentication();
        }
    }
}
