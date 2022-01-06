
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;

namespace Infrastructure.Test
{
    public class DatabaseBaseTest : IDisposable
    {
        protected readonly DatabaseContext context;

        public DatabaseBaseTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase("TestDatabase").Options;
            context = new DatabaseContext(options);
            context.Database.EnsureCreated();
            DatabaseInitializer.Initialize(context);
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
