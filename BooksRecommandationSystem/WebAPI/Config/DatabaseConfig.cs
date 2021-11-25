using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence.Context;

namespace WebAPI.Config
{
    /// <summary>
    ///     Database related configurations
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        ///     Setup ASP.NET Core Identity DB, including connection string, Identity options, token providers, and token services, etc..
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void SetupIdentityDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            /*services.AddDbContext<ApplicationUserContext>(options =>
                //options.UseSqlServer(configuration.GetConnectionString("CleanArchitectureIdentity"))
                options.UseInMemoryDatabase("CleanArchitectureIdentity")
                );*/

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddSignInManager<SignInManager<ApplicationUser>>()
                    .AddEntityFrameworkStores<ApplicationUserContext>();
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

            // services required using Identity
            //services.AddScoped<ITokenService, TokenService>();
        }
    }
}
