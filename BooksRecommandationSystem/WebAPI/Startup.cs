using Application;
using Domain.AuthModels;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Extensions;
using WebAPI.Config;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            // Strongly-typed configurations using IOptions
            services.Configure<Token>(Configuration.GetSection("token"));

            // Identity DB for Identity
            services.SetupIdentityDatabase(Configuration);

            // HttpContext
            services.AddHttpContextAccessor();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = false;
                config.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddEndpointsApiExplorer();

            services.AddApplication();

            services.AddPersistence(Configuration);

            services.AddControllers(options =>
            {
                options.Conventions.Add(new NamespaceConvention());
            });

            services.AddSwaggerGen(config =>
            {
                var title = "The Book Dealer";
                var description = "Your trusted Book Dealer that always gets you what you need.";

                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = title + " v1",
                    Description = description
                });

                config.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = title + " v2",
                    Description = description
                });
            }
      );
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // ONLY automatically create development databases
                // app.EnsureCosmosDbIsCreated();
                // app.SeedToDoContainerIfEmptyAsync().Wait();
                // Optional: auto-create and seed Identity DB
                app.EnsureIdentityDbIsCreated();
                app.SeedIdentityDataAsync().Wait();
            }
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}