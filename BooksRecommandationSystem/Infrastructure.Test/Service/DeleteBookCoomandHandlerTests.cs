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
    public class DeleteBookCommandHandlerTests
    {
        private readonly DeleteBookCommandHandler handler;
        private readonly IBookRepository repository;

        public DeleteBookCommandHandlerTests()
        {
            this.repository = A.Fake<IBookRepository>();
            this.handler = new DeleteBookCommandHandler(this.repository);
        }

        [Fact]
        public async void Given_DeleteBookCommand_WhenHandleIsCalled_ThenDeleteAsyncBookIsCalled()
        {
            Book book = new Book
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(book);

            await handler.Handle(new DeleteBookCommand
            (
                new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            ), default);

            A.CallTo(() => repository.DeleteAsync(A<Book>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Given_DeleteBookCommand_When_HandleIsCalledAndBookIsNull_Then_ShouldThrowException()
        {
            Book book = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(book);

            Func<Task> action = async () => await handler.Handle(new DeleteBookCommand
            (
                new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            ), default);

            _ = action.Should().ThrowAsync<Exception>();
        }

    }
}
