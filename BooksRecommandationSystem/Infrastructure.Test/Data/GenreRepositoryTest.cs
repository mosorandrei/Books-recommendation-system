using Domain.Entities;
using FluentAssertions;
using Persistence.v2;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Data
{
    public class GenreRepositoryTest : GenreDatabaseBaseTest
    {
        private readonly Repository<Genre> repository;
        private readonly Genre newGenre;
        private readonly Genre copyGenre;
        private readonly Genre nullIdGenre;

        public GenreRepositoryTest()
        {
            repository = new Repository<Genre>(context);
            newGenre = new Genre()
            {
                Id = Guid.Parse("fc3290a4-1d23-4697-b70f-5d58f8ab10e0"),
                Name = "Comedy"
            };

            copyGenre = new Genre()
            {
                Id = Guid.Parse("fc3290a4-1d23-4697-b70f-5d58f8ab10e0"),
                Name = "Action"
            };

            nullIdGenre = new Genre()
            {
                Id = Guid.Empty,
                Name = "Drama"
            };
        }

        [Fact]
        public async Task GivenNewGenreWhenGenreIsNotNullThenAddAsyncShouldReturnANewGenre()
        {
            var result = await repository.AddAsync(newGenre);
            result.Should().BeOfType<Genre>();
        }

        [Fact]
        public void GivenNewGenreWhenGenreIsNullThenAddSyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.AddAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenNewGenreWhenGenreIsNotNullThenUpdateAsyncShouldReturnANewAuthor()
        {
            await repository.AddAsync(newGenre);
            var result = await repository.DeleteAsync(newGenre);
            result.Should().BeOfType<Genre>();
        }

        [Fact]
        public void GivenNewGenreWhenGenreIsNullThenUpdateAsyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.UpdateAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenNewGenreWhenGenreIsNotNullThenDeleteAsyncShouldReturnANewAuthor()
        {
            await repository.AddAsync(newGenre);
            var result = await repository.DeleteAsync(newGenre);
            result.Should().BeOfType<Genre>();
        }

        [Fact]
        public void GivenNewGenreWhenGenreIsNullThenDeleteASyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.DeleteAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenTwoGenresWhenTheyHaveTheSameGuidThenShouldThrowException()
        {
            await repository.AddAsync(newGenre);
            _ = repository.Invoking(r => r.AddAsync(copyGenre)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public void GivenNewGenreWhenGuidIsEmptyThenShouldThrowException()
        {
            _ = repository.Invoking(r => r.AddAsync(nullIdGenre)).Should().ThrowAsync<Exception>();
        }
    }
}
