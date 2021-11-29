using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service
{
    public class CreateBookCommandHandlerTests
    {
        private readonly CreateBookCommandHandler handler;
        private readonly IBookRepository repository;

        public CreateBookCommandHandlerTests()
        {
            this.repository = A.Fake<IBookRepository>();
            this.handler = new CreateBookCommandHandler(this.repository);
        }

        [Fact]
        public async Task Given_CreateBookCommandHandler_When_HandleIsCalled_Then_AddAsyncBookIsCalled()
        {
            await handler.Handle(new CreateBookCommand(), default);
            A.CallTo(() => repository.AddAsync(A<Book>._)).MustHaveHappenedOnceExactly();

        }
    }
}
