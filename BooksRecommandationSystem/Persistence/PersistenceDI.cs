using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
//using Persistence.v1;
using Persistence.v2;

namespace Persistence
{
    public static class PersistenceDI
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookContext>(options => options.UseSqlite("Data Source=MyBooks.db"));
            services.AddDbContext<GenreContext>(options => options.UseSqlite("Data Source=MyGenres.db"));
            services.AddDbContext<AuthorContext>(options => options.UseSqlite("Data Source=MyAuthors.db"));
            services.AddDbContext<UserContext>(options => options.UseSqlite("Data Source=MyUsers.db"));
            services.AddDbContext<ApplicationUserContext>(options => options.UseSqlite("Data Source=MyApplicationUsers.db"));
            // register implementations related to repository/generic implementation
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
        }
    }
}
