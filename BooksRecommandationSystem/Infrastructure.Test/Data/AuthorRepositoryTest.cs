using Domain.Entities;
using FluentAssertions;
using Persistence.v1;
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
        public async Task Given_NewAuthor_WhenAuthorIsNotNull_Then_AddAsyncShouldReturnANewAuthor()
        {
            var result = await repository.AddAsync(newAuthor);
            result.Should().BeOfType<Author>();
        }
    }
}
