using GraphQL.Server.Ui.Playground;
using Hilfswerk.EntityFramework;
using Hilfswerk.GraphApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _webHostEnvironment = environment;
            environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSimpleTokenAuthentication(_configuration.GetSection("Authentication"));
            services.AddHilfswerkGraphApi(opt =>
            {
                if (!_webHostEnvironment.IsDevelopment())
                {
                    opt.AuthorizationPolicy = "DefaultPolicy";
                }
            });
            services.AddHilfswerkEntityFramework();

            services.AddDbContext<HilfswerkDbContext>(opt =>
            {
                var dbPath = Path.Combine(_webHostEnvironment.WebRootPath, "App_Data", "hilfswerk.db");
                var connection = new SqliteConnection($"Data Source={dbPath}");
                connection.Open();
                connection.CreateFunction("contains_ignore_case", (Func<string, string, bool>)HilfswerkDbContext.ContainsIgnoreCase);
                opt.UseSqlite(connection,
                    sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
                if (_webHostEnvironment.IsDevelopment())
                {
                    opt.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: false);
                }
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
