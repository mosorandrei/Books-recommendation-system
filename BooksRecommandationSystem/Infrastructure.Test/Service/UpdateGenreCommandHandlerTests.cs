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
    public class UpdateGenreCommandHandlerTests
    {
        private readonly UpdateGenreCommandHandler handler;
        private readonly IGenreRepository repository;

        public UpdateGenreCommandHandlerTests()
        {
            repository = A.Fake<IGenreRepository>();
            handler = new UpdateGenreCommandHandler(repository);
        }

        [Fact]
        public async void Given_UpdateGenreCommand_When_HandleIsCalled_Then_ShouldUpdateGenre()
        { 
            Genre genre = new Genre()
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("328eda97-05f0-4b4a-8532-800d292282b4"))).Returns(genre);

            await handler.Handle(new UpdateGenreCommand
            {
                Id = new Guid("328eda97-05f0-4b4a-8532-800d292282b4")
            }, default);

            A.CallTo(() => repository.UpdateAsync(A<Genre>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Given_UpdateGenreCommand_When_HandleIsCalledAndGenreIsNull_Then_ShouldThrowException()
        {
            Genre genre = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("328eda97-05f0-4b4a-8532-800d292282b4"))).Returns(genre);

            Func<Task> action = async () => await handler.Handle(new UpdateGenreCommand
            {
                Id = new Guid("328eda97-05f0-4b4a-8532-800d292282b4")
            }, default);

            _ = action.Should().ThrowAsync<Exception>();
        }

    }
}
