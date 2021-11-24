using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class UserContext : DbContext
    {
        public UserContext()
        {
        }
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User>? Users { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=MyUsers.db");
        //}
    }
}
