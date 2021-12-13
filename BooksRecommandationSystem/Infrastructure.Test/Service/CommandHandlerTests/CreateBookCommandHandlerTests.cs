using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using System;
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
        public async Task GivenCreateBookCommandHandlerWhenHandleIsCalledThenAddAsyncBookIsCalled()
        {
            await handler.Handle(new CreateBookCommand(), default);
            A.CallTo(() => repository.AddAsync(A<Book>._)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task GivenCreateBookCommandHandlerWhenHandleIsCalledAndGuidExistsThenAddAsyncBookIsNotCalled()
        {
            Book book = new()
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            Book book2 = new()
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(book);
            await handler.Handle(new CreateBookCommand(), default);
            A.CallTo(() => repository.AddAsync(book2)).MustNotHaveHappened();
        }
    }
}
