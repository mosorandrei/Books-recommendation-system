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
        private readonly Author copyAuthor;
        private readonly Author nullIdAuthor;

        public AuthorRepositoryTest()
        {
            repository = new Repository<Author>(context);
            newAuthor = new Author()
            {
                Id = Guid.Parse("2e65f40e-3067-4ef2-91dc-cda91ae32dfb"),
                FirstName = "Mihail",
                LastName = "Sadoveanu"
            };

            copyAuthor = new Author()
            {
                Id = Guid.Parse("2e65f40e-3067-4ef2-91dc-cda91ae32dfb"),
                FirstName = "Mihai",
                LastName = "Eminescu"
            };

            nullIdAuthor = new Author()
            {
                Id = Guid.Empty,
                FirstName = "Mihai",
                LastName = "Eminescu"
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
        [Fact]
        public async Task GivenNewAuthorWhenAuthorIsNotNullThenUpdateAsyncShouldReturnANewAuthor()
        {
            await repository.AddAsync(newAuthor);
            var result = await repository.DeleteAsync(newAuthor);
            result.Should().BeOfType<Author>();
        }

        [Fact]
        public void GivenNewAuthorWhenAuthorIsNullThenUpdateAsyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.UpdateAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenNewAuthorWhenAuthorIsNotNullThenDeleteAsyncShouldReturnANewAuthor()
        {
            await repository.AddAsync(newAuthor);
            var result = await repository.DeleteAsync(newAuthor);
            result.Should().BeOfType<Author>();
        }

        [Fact]
        public void GivenNewAuthorWhenAuthorIsNullThenDeleteASyncShouldReturnThrowArgumentNullException()
        {
            _ = repository.Invoking(r => r.DeleteAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenTwoAuthorsWhenTheyHaveTheSameGuidThenShouldThrowException()
        {
            await repository.AddAsync(newAuthor);
            _ = repository.Invoking(r => r.AddAsync(copyAuthor)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public void GivenNewAuthorWhenGuidIsEmptyThenShouldThrowException()
        {
            _ = repository.Invoking(r => r.AddAsync(nullIdAuthor)).Should().ThrowAsync<Exception>();
        }
    }
}
