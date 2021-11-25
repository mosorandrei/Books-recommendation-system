using Domain.Entities;
using FluentAssertions;
using Persistence.v2;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Data
{
    public class AuthorRepositoryTest : AuthorDatabaseBaseTest
    {
        private readonly Repository<Author> repository;
        private readonly Author newAuthor;

        public AuthorRepositoryTest()
        {
            repository = new Repository<Author>(context);
            newAuthor = new Author()
            {
                Id = Guid.Parse("2e65f40e-3067-4ef2-91dc-cda91ae32dfb"),
                FirstName = "Mihail",
                LastName = "Sadoveanu"
            };
        }

        [Fact]
        public async Task GivenNewAuthorWhenAuthorIsNotNullThenAddAsyncShouldReturnANewAuthor()
        {
            var result = await repository.AddAsync(newAuthor);
            result.Should().BeOfType<Author>();
        }

        [Fact]
        public void GivenNewAuthorWhenAuthorIsNullThenAddSyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.AddAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
