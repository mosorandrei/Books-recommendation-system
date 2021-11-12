using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class AuthorContext : DbContext
    {
        //public AuthorContext()
        //{
        //}
        public AuthorContext(DbContextOptions<AuthorContext> options) : base(options)
        {
        }
        public DbSet<Author>? Authors { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=MyAuthors.db");
        //}
    }
}
