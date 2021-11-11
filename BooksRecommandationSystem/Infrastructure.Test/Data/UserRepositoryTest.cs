using Domain.Entities;
using FluentAssertions;
using Persistence.v1;
using System;
using Xunit;

namespace Infrastructure.Test.Data
{
    public class UserRepositoryTest : UserDatabaseBaseTest
    {
        private readonly Repository<User> repository;
        private readonly User newUser;

        public UserRepositoryTest()
        {
            repository = new Repository<User>(context);
            newUser = new User()
            {
                Id = Guid.Parse("14bdf029-f56c-4532-9f20-3a5add7406fa"),
                Username = "Alex",
                Password = "*****"
            };
        }

        [Fact]
        public async void Given_NewUser_WhenUserIsNotNull_Then_AddAsyncShouldReturnANewUser()
        {
            var result = await repository.AddAsync(newUser);
            result.Should().BeOfType<User>();
        }
    }
}
