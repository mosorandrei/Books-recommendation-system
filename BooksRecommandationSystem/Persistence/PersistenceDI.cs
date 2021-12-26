using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.v2;

namespace Persistence
{
    public static class PersistenceDI
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite("Data Source=MyDatabase.db"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IReadingStatusRepository, ReadingStatusRepository>();
            services.AddTransient<IBookGenreAssociationRepository, BookGenreAssociationRepository>();
            services.AddTransient<IBookAuthorAssociationRepository, BookAuthorAssociationRepository>();
        }
    }
}
