using Application;
using Domain.AuthModels;
using Domain.CustomExceptionMiddleware;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Context;
using Persistence.Extensions;
using System.Net;
using System.Text;

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
            // Add Cors
            services.AddCors(o => o.AddPolicy("FEPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

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

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below!"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                    }, Array.Empty<string>()
                } });
            }
      );
            // Strongly-typed configurations using IOptions
            services.Configure<Token>(Configuration.GetSection("token"));

            // Identity DB for Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddSignInManager<SignInManager<ApplicationUser>>()
                    .AddEntityFrameworkStores<DatabaseContext>();
            services.Configure<IdentityOptions>(
                options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                    // Identity : Default password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                });

            Token token = Configuration.GetSection("token").Get<Token>();
            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = token.Audience,
                    ValidIssuer = token.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s: token.Secret is not null ? token.Secret : ""))
                };
            });

            // HttpContext
            services.AddHttpContextAccessor();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Auto-create and seed Identity DB
                app.EnsureIdentityDbIsCreated();
                app.SeedIdentityDataAsync().Wait();
            }
            app.UseMiddleware(typeof(ExceptionMiddleware));
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("FEPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}