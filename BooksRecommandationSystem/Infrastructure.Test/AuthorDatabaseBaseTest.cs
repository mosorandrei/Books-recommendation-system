
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;

namespace Infrastructure.Test
{
    public class AuthorDatabaseBaseTest : IDisposable
    {
        protected readonly AuthorContext context;

        public AuthorDatabaseBaseTest()
        {
            var options = new DbContextOptionsBuilder<AuthorContext>().UseInMemoryDatabase("AuthorTestDatabase").Options;
            context = new AuthorContext(options);
            context.Database.EnsureCreated();
            AuthorDatabaseInitializer.Initialize(context);
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
