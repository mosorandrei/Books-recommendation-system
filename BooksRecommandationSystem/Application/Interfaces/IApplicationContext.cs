using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<Domain.Entities.Book> Books { get; set; }
        Task<int> SaveChangesAsync();
    }
}
