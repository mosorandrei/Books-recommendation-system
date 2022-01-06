using Domain.Entities;
using FluentAssertions;
using Persistence.v2;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Data
{
    public class BookRepositoryTest : DatabaseBaseTest
    {
        private readonly Repository<Book> repository;
        private readonly Book newBook;
        private readonly Book copyBook;

        public BookRepositoryTest()
        {
            repository = new Repository<Book>(context);
            newBook = new Book()
            {
                Id = Guid.Parse("cda75f32-15d8-418b-b174-6ee981a537f0"),
                Title = "Divergent",
                Rating = 5,
                NumberOfReviews = 1,
                Description = "",
                PublicationDate = DateTime.Now, //for testing purposes
                ImageUri = new Uri("https://bit.ly/3xlV5i2"),
                DownloadUri = new Uri("http://bit.ly/Reads_Divergent")
            };
            copyBook = new Book()
            {
                Id = Guid.Parse("cda75f32-15d8-418b-b174-6ee981a537f0"),
                Title = "John Wick",
                Rating = 5,
                NumberOfReviews = 1,
                Description = "",
                PublicationDate = DateTime.Now, //for testing purposes
                ImageUri = new Uri("https://bit.ly/3xlV5i2"),
                DownloadUri = new Uri("http://bit.ly/Reads_Divergent")
            };
            newBook = new Book()
            {
                Id = Guid.Empty,
                Title = "Film",
                Rating = 5,
                NumberOfReviews = 1,
                Description = "",
                PublicationDate = DateTime.Now, //for testing purposes
                ImageUri = new Uri("https://bit.ly/3xlV5i2"),
                DownloadUri = new Uri("http://bit.ly/Reads_Divergent")
            };
        }

        [Fact]
        public void GivenNewBookWhenBookIsNullThenAddSyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.AddAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void GivenNewBookWhenBookIsNullThenUpdateAsyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.UpdateAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void GivenNewBookWhenBookIsNullThenDeleteASyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.DeleteAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenTwoBooksWhenTheyHaveTheSameGuidThenShouldThrowException()
        {
            await repository.AddAsync(newBook);
            _ = repository.Invoking(r => r.AddAsync(copyBook)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public void GivenNewBookWhenGuidIsEmptyThenShouldThrowException()
        {
            _ = repository.Invoking(r => r.AddAsync(null)).Should().ThrowAsync<Exception>();
        }
    }
}
