using GraphQL.Server.Ui.Playground;
using Hilfswerk.EntityFramework;
using Hilfswerk.GraphApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSimpleTokenAuthentication(_configuration.GetSection("Authentication"));
            services.AddHilfswerkGraphApi();
            services.AddHilfswerkEntityFrameworkStores();

            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<HilfswerkDbContext>(opt =>
            {
                opt.UseInMemoryDatabase(databaseName: "hilfswerk");
                opt.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);
            });

            services.AddAuthorization(o =>
            {
                o.DefaultPolicy = new AuthorizationPolicyBuilder(new[] { JwtBearerDefaults.AuthenticationScheme })
                    .RequireAuthenticatedUser()
                    .Build();
            });
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("P", builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(_configuration.GetValue<string>("FrontendUrl"), "https://localhost:44391"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("P");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGraphQL<HilfswerkSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
