using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public partial class ApplicationUserContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserContext()
        {
        }
        public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser>? Authors { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyApplicationUsers.db");
        }
    }
}
