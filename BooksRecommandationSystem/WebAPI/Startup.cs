using Application;
using Persistence;
using Microsoft.OpenApi.Models;

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
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
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