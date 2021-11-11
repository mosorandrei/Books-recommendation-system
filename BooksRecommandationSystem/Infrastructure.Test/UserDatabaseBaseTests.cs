
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;

namespace Infrastructure.Test
{
    public class UserDatabaseBaseTest : IDisposable
    {
        protected readonly UserContext context;

        public UserDatabaseBaseTest()
        {
            var options = new DbContextOptionsBuilder<UserContext>().UseInMemoryDatabase("UserTestDatabase").Options;
            context = new UserContext(options);
            context.Database.EnsureCreated();
            UserDatabaseInitializer.Initialize(context);
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
