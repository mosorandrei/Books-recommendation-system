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
    public class UpdateAuthorCommandHandlerTests
    {
        private readonly UpdateAuthorCommandHandler handler;
        private readonly IAuthorRepository repository;

        public UpdateAuthorCommandHandlerTests()
        {
            repository = A.Fake<IAuthorRepository>();
            handler = new UpdateAuthorCommandHandler(repository);
        }

        [Fact]
        public async Task GivenUpdateAuthorCommandWhenHandleIsCalledThenShouldReturnUpdateAuthor()
        {
            Author author = new()
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(author);

            await handler.Handle(new UpdateAuthorCommand
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            }, default);

            A.CallTo(() => repository.UpdateAsync(A<Author>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GivenUpdateAuthorCommandWhenHandleIsCalledAndAuthorIsNullThenShouldThrowException()
        {
            Author? author = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(author);

            Func<Task> action = async () => await handler.Handle(new UpdateAuthorCommand
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            }, default);

            _ = action.Should().ThrowAsync<Exception>();
        }

    }
}
