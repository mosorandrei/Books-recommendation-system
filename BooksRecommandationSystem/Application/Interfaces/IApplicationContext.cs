using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<Domain.Entities.Book> Books { get; set; }
        DbSet<Domain.Entities.Genre> Genres { get; set; }
        DbSet<Domain.Entities.Author> Authors { get; set; }
        Task<int> SaveChangesAsync();
    }
}
