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
    public class DeleteAuthorCommandHandlerTests
    {
        private readonly DeleteAuthorCommandHandler handler;
        private readonly IAuthorRepository repository;

        public DeleteAuthorCommandHandlerTests()
        {
            this.repository = A.Fake<IAuthorRepository>();
            this.handler = new DeleteAuthorCommandHandler(this.repository);
        }

        [Fact]
        public async Task GivenDeleteAuthorCommandWhenHandleIsCalledThenDeleteAsyncAuthorIsCalled()
        {
            Author author = new()
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(author);

            await handler.Handle(new DeleteAuthorCommand
            (
                new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            ), default);

            A.CallTo(() => repository.DeleteAsync(A<Author>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GivenDeleteAuthorCommandWhenHandleIsCalledAndAuthorIsNullThenShouldThrowException()
        {
            Author? author = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(author);

            Func<Task> action = async () => await handler.Handle(new DeleteAuthorCommand
            (
                new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            ), default);

            _ = action.Should().ThrowAsync<Exception>();
        }

    }
}
