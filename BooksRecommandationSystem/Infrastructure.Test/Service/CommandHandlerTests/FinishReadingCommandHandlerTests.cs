

using Application.Features.Commands;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.CommandHandlerTests
{
    public class FinishReadingCommandHandlerTests
    {
        private readonly FinishReadingCommandHandler handler;
        private readonly IReadingStatusRepository repository;
        public FinishReadingCommandHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            handler = new FinishReadingCommandHandler(repository);
        }
        [Fact]
        public async Task GivenFinishReadingCommandWhenHandleIsCalledThenFinishReadingIsCalled()
        {
            FinishReadingCommand command = new FinishReadingCommand();
            command.UserId = "ID";
            await handler.Handle(command, default);
            A.CallTo(() => repository.FinishReading(command.UserId, command.BookId)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public void GivenFinishReadingCommandWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            FinishReadingCommand command = new FinishReadingCommand();
            command.UserId = null;
            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }
    }
}

