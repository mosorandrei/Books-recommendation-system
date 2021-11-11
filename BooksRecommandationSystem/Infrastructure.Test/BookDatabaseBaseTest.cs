
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;

namespace Infrastructure.Test
{
    public class BookDatabaseBaseTest : IDisposable
    {
        protected readonly BookContext context;

        public BookDatabaseBaseTest()
        {
            var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase("BookTestDatabase").Options;
            context = new BookContext(options);
            context.Database.EnsureCreated();
            BookDatabaseInitializer.Initialize(context);
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
