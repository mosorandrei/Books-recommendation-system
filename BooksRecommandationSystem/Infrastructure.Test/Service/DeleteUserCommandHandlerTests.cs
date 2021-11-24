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
    public class DeleteUserCommandHandlerTests
    {
        private readonly DeleteUserCommandHandler handler;
        private readonly IUserRepository repository;

        public DeleteUserCommandHandlerTests()
        {
            this.repository = A.Fake<IUserRepository>();
            this.handler = new DeleteUserCommandHandler(this.repository);
        }

        [Fact]
        public async void Given_DeleteUserCommand_WhenHandleIsCalled_ThenDeleteAsyncUserIsCalled()
        {
            User user = new User
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(user);

            await handler.Handle(new DeleteUserCommand
            (
                new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            ), default);

            A.CallTo(() => repository.DeleteAsync(A<User>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Given_DeleteUserCommand_When_HandleIsCalledAndUserIsNull_Then_ShouldThrowException()
        {
            User user = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(user);

            Func<Task> action = async () => await handler.Handle(new DeleteUserCommand
            (
                new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            ), default);

            _ = action.Should().ThrowAsync<Exception>();
        }

    }
}
