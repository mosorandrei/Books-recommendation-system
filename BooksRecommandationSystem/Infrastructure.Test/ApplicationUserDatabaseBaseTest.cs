using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Test
{
    public class ApplicationUserDatabaseBaseTest : IDisposable
    {
        protected readonly ApplicationUserContext context;
        public ApplicationUserDatabaseBaseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationUserContext>().UseInMemoryDatabase("AuthorTestDatabase").Options;
            context = new ApplicationUserContext(options);
            context.Database.EnsureCreated();
            ApplicationUserDatabaseInitializer.Initialize(context);
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
