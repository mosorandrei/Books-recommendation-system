

using Domain.Entities;
using FluentAssertions;
using Persistence.v1;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Data
{
    public class BookRepositoryTest : BookDatabaseBaseTest
    {
        private readonly Repository<Book> repository;
        private readonly Book newBook;

        public BookRepositoryTest()
        {
            repository = new Repository<Book>(context);
            newBook = new Book() 
            {
                Id = Guid.Parse("cda75f32-15d8-418b-b174-6ee981a537f0"),
                Title = "Titanic",
                Rating = 10,
                Description = "",
                PublicationDate = DateTime.Now,//for testing purposes
                ImageUri = new Uri("https://en.wikipedia.org/wiki/Titanic")
            };
        }

        [Fact]
        public async Task Given_NewBook_WhenBookIsNotNull_Then_AddAsyncShouldReturnANewBook()
        {
            var result = await repository.AddAsync(newBook);
            result.Should().BeOfType<Book>();
        }
    }
}
