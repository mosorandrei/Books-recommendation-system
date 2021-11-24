using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class GenreContext : DbContext
    {
        public GenreContext()
        {
        }
        public GenreContext(DbContextOptions<GenreContext> options) : base(options)
        {
        }
        public DbSet<Genre>? Genres { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=MyGenres.db");
        //}
    }
}
