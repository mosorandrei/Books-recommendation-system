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
    public class UpdateBookCommandHandlerTests
    {
        private readonly UpdateBookCommandHandler handler;
        private readonly IBookRepository repository;

        public UpdateBookCommandHandlerTests()
        {
            repository = A.Fake<IBookRepository>();
            handler = new UpdateBookCommandHandler(repository);
        }

        [Fact]
        public async Task GivenUpdateBookCommandWhenHandleIsCalledThenShouldUpdateBook()
        {
            Book book = new()
            {
                Id = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"))).Returns(book);

            await handler.Handle(new UpdateBookCommand
            {
                Id = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
            }, default);

            A.CallTo(() => repository.UpdateAsync(A<Book>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GivenUpdateBookCommandWhenHandleIsCalledAndBookIsNullThenShouldThrowException()
        {
            Book? book = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"))).Returns(book);

            Func<Task> action = async () => await handler.Handle(new UpdateBookCommand
            {
                Id = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
            }, default);

            _ = action.Should().ThrowAsync<Exception>();
        }

    }
}
