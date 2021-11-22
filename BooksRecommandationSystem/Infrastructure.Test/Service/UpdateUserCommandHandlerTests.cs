using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service
{
    public class UpdateUserCommandHandlerTests
    {
        private readonly UpdateUserCommandHandler handler;
        private readonly IUserRepository repository;

        public UpdateUserCommandHandlerTests()
        {
            repository = A.Fake<IUserRepository>();
            handler = new UpdateUserCommandHandler(repository);
        }

        [Fact]
        public async void Given_UpdateUserCommand_When_HandleIsCalled_Then_ShouldUpdateUser()
        {
            User user = new User
            {
                Id = new Guid("07cf3eb9-b32a-4942-88ef-860c14d76b58")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("07cf3eb9-b32a-4942-88ef-860c14d76b58"))).Returns(user);

            await handler.Handle(new UpdateUserCommand
            {
                Id = new Guid("07cf3eb9-b32a-4942-88ef-860c14d76b58")
            }, default);

            A.CallTo(() => repository.UpdateAsync(A<User>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Given_UpdateUserCommand_When_HandleIsCalledAndUserIsNull_Then_ShouldThrowException()
        {
            User user = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("07cf3eb9-b32a-4942-88ef-860c14d76b58"))).Returns(user);

            Func<Task> action = async () => await handler.Handle(new UpdateUserCommand
            {
                Id = new Guid("07cf3eb9-b32a-4942-88ef-860c14d76b58")
            }, default);

            _ = action.Should().ThrowAsync<Exception>();
        }

    }
}
