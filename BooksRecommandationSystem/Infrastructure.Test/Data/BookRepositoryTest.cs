using Domain.Entities;
using FluentAssertions;
using Persistence.v2;
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
                Title = "Divergent",
                Rating = 10,
                Description = "",
                PublicationDate = DateTime.Now, //for testing purposes
                ImageUri = new Uri("https://bit.ly/3xlV5i2"),
                DownloadUri = new Uri("http://bit.ly/Reads_Divergent")
            };
        }

        [Fact]
        public async Task GivenNewBookWhenBookIsNotNullThenAddAsyncShouldReturnANewBook()
        {
            var result = await repository.AddAsync(newBook);
            result.Should().BeOfType<Book>();
        }

        [Fact]
        public void GivenNewBookWhenBookIsNullThenAddSyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.AddAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
