using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<Domain.Entities.Book> Books { get; set; }
        DbSet<Domain.Entities.Genre> Genres { get; set; }
        DbSet<Domain.Entities.Author> Authors { get; set; }
        DbSet<Domain.Entities.User> Users { get; set; }
        DbSet<Domain.Entities.ApplicationUser> ApplicationUsers { get; set; }
        Task<int> SaveChangesAsync();
    }
}
