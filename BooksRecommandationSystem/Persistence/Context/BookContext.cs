using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class BookContext : DbContext
    {
        public BookContext()
        {
        }
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }
        public DbSet<Book>? Books { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyBooks.db");
        }
    }
}
