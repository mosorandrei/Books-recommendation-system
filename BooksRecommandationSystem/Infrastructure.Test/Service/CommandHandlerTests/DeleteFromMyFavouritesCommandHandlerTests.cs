

using Application.Features.Commands;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.CommandHandlerTests
{
    public class DeleteFromMyFavouritesCommandHandlerTests
    {
        private readonly DeleteFromMyFavouritesCommandHandler handler;
        private readonly IReadingStatusRepository repository;
        public DeleteFromMyFavouritesCommandHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            handler = new DeleteFromMyFavouritesCommandHandler(repository);
        }
        [Fact]
        public async void GivenDeleteFromMyFavouritesCommandWhenHandleIsCalledThenShouldDeleteFromFavorites()
        {
            DeleteFromMyFavouritesCommand command = new DeleteFromMyFavouritesCommand();
            command.UserId = "ID";
            await handler.Handle(command, default);
            A.CallTo(() => repository.DeleteFromMyFavourites(command.UserId, command.BookId)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public void GivenDeleteFromMyFavouritesCommandWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            DeleteFromMyFavouritesCommand command = new DeleteFromMyFavouritesCommand();
            command.UserId = null;
            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }
    }
}
