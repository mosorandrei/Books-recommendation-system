using Application.Features.Commands;
using Application.Interfaces;
using FakeItEasy;
using Xunit;

namespace Infrastructure.Test.Service.CommandHandlerTests
{
    public class RateBookCommandHandlerTests
    {
        private readonly RateBookCommandHandler handler;
        private readonly IReadingStatusRepository repository;
        private readonly IBookRepository bookRepository;

        public RateBookCommandHandlerTests()
        {
            repository = A.Fake<IReadingStatusRepository>();
            bookRepository = A.Fake<IBookRepository>();
            handler = new RateBookCommandHandler(repository, bookRepository);
        }
        /*
        [Fact]

        public async void GivenRateBookCommandHandlerWhenHandleIsCalledThenRateBookIsCalled()
        {   RateBookCommand command = new RateBookCommand();
            command.UserId = "user";
            //command.Score = 1;
            //command.BookId = new System.Guid();
            await handler.Handle(command, default);
            A.CallTo(() => repository.RateBook(command.UserId,command.BookId,command.Score)).MustHaveHappenedOnceExactly();
           
        }*/
    }
}
