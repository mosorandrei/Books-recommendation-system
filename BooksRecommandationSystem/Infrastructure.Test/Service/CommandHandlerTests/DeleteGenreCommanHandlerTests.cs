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
    public class DeleteGenreCommandHandlerTests
    {
        private readonly DeleteGenreCommandHandler handler;
        private readonly IGenreRepository repository;

        public DeleteGenreCommandHandlerTests()
        {
            this.repository = A.Fake<IGenreRepository>();
            this.handler = new DeleteGenreCommandHandler(this.repository);
        }

        [Fact]
        public async void Given_DeleteGenreCommand_WhenGenreIsCalled_ThenDeleteAsyncGenreIsCalled()
        {
            Genre genre = new Genre
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(genre);

            await handler.Handle(new DeleteGenreCommand
            (
                new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            ), default);

            A.CallTo(() => repository.DeleteAsync(A<Genre>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Given_DeleteGenreCommand_When_HandleIsCalledAndGenreIsNull_Then_ShouldThrowException()
        {
            Genre genre = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(genre);

            Func<Task> action = async () => await handler.Handle(new DeleteGenreCommand
            (
                new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            ), default);

            _ = action.Should().ThrowAsync<Exception>();
        }

    }
}
