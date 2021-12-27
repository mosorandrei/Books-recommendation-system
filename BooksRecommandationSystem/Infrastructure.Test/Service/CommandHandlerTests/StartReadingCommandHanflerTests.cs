

using Application.Features.Commands;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.CommandHandlerTests
{
    public class StartReadingCommandHandlerTests
    {
        private readonly StartReadingCommandHandler handler;
        private readonly IReadingStatusRepository repository;
        public StartReadingCommandHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            handler = new StartReadingCommandHandler(repository);
        }
        [Fact]
        public async void GivenStartReadingCommandWhenHandleIsCalledThenStartReadingIsCalled()
        {
            StartReadingCommand command = new StartReadingCommand();
            command.UserId = "ID";
            await handler.Handle(command, default);
            A.CallTo(() => repository.StartReading(command.UserId, command.BookId)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public void GivenStartReadingCommandWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            StartReadingCommand command = new StartReadingCommand();
            command.UserId = null;
            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }
    }
}

