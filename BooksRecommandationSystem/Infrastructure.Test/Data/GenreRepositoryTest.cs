

using Domain.Entities;
using FluentAssertions;
using Persistence.v1;
using System;
using Xunit;

namespace Infrastructure.Test.Data
{
    public class GenreRepositoryTest : GenreDatabaseBaseTest
    {
        private readonly Repository<Genre> repository;
        private readonly Genre newGenre;

        public GenreRepositoryTest()
        {
            repository = new Repository<Genre>(context);
            newGenre = new Genre()
            {
                Id = Guid.Parse("fc3290a4-1d23-4697-b70f-5d58f8ab10e0"),
                Name = "Comedy"
            };
        }

        [Fact]
        public async void Given_NewGenre_WhenGenreIsNotNull_Then_AddAsyncShouldReturnANewGenre()
        {
            var result = await repository.AddAsync(newGenre);
            result.Should().BeOfType<Genre>();
        }
    }
}
