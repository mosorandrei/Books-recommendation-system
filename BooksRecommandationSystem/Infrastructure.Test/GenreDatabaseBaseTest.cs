
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;

namespace Infrastructure.Test
{
    public class GenreDatabaseBaseTest : IDisposable
    {
        protected readonly GenreContext context;

        public GenreDatabaseBaseTest()
        {
            var options = new DbContextOptionsBuilder<GenreContext>().UseInMemoryDatabase("GenreTestDatabase").Options;
            context = new GenreContext(options);
            context.Database.EnsureCreated();
            GenreDatabaseInitializer.Initialize(context);
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
