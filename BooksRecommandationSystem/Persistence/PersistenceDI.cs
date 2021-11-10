using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.v1;

namespace Persistence
{
    public static class PersistenceDI
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<BookContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyConnection"), b => b.MigrationsAssembly(typeof(BookContext).Assembly.FullName)));
            services.AddDbContext<BookContext>(options => options.UseSqlite("Data Source=MyBooks.db"));
            services.AddDbContext<GenreContext>(options => options.UseSqlite("Data Source=MyGenres.db"));
            // services.AddScoped<IApplicationContext, BookContext>();
            // register implementations related to repository/generic implementation
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
        }
    }
}
