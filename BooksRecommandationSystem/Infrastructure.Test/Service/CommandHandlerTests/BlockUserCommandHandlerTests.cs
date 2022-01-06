using Application.Features.Commands;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.CommandHandlerTests
{
    public class BlockUserCommandHandlerTests
    {
        private readonly BlockUserCommandHandler handler;
        private readonly ITokenRepository repository;

        public BlockUserCommandHandlerTests()
        {
            repository = A.Fake<ITokenRepository>();
            handler = new BlockUserCommandHandler(repository);
        }

        [Fact]

        public async void GivenBlockUserCommandHandlerWhenHandleIsCalledThenBlockUserIsCalled()
        {   
            BlockUserCommand command = new BlockUserCommand();
            command.UserId = "user";
            await handler.Handle(command, default);
            A.CallTo(() => repository.BlockUser(command.UserId)).MustHaveHappenedOnceExactly();
        }

        [Fact]

        public void GivenBlockUserCommandHandlerWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            BlockUserCommand command = new BlockUserCommand();
            command.UserId = "";
            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();
        }

    }
}
