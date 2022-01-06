

using Application.Features.Commands;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.CommandHandlerTests
{
    public class AddToMyFavouritesCommandHandlerTests
    {
        private readonly AddToMyFavouritesCommandHandler handler;
        private readonly IReadingStatusRepository repository;
        public AddToMyFavouritesCommandHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            handler = new AddToMyFavouritesCommandHandler(repository);
        }
        [Fact]
        public async Task GivenAddToMyFavouritesCommandWhenHandleIsCalledThenShouldAddToFavorites()
        {
            AddToMyFavouritesCommand command = new AddToMyFavouritesCommand();
            command.UserId = "ID";
            await handler.Handle(command, default);
            A.CallTo(() => repository.AddToMyFavourites(command.UserId, command.BookId)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public void GivenAddToMyFavouritesCommandWhenHandleIsCalledAndUserIdIsNullThenShouldThrowException()
        {
            AddToMyFavouritesCommand command = new AddToMyFavouritesCommand();
            command.UserId = null;
            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();

        }
    }
}
