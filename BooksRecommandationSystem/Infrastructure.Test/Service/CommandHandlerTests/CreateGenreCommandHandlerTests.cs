using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service
{
    public class CreateGenreCommandHandlerTests
    {
        private readonly CreateGenreCommandHandler handler;
        private readonly IGenreRepository repository;

        public CreateGenreCommandHandlerTests()
        {
            this.repository = A.Fake<IGenreRepository>();
            this.handler = new CreateGenreCommandHandler(this.repository);
        }

        [Fact]
        public async Task GivenCreateGenreCommandHandlerWhenHandleIsCalledThenAddAsyncGenreIsCalled()
        {
            await handler.Handle(new CreateGenreCommand(), default);
            A.CallTo(() => repository.AddAsync(A<Genre>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GivenCreateGenreCommandHandlerWhenHandleIsCalledAndGuidExistsThenAddAsyncGenreIsNotCalled()
        {
            Genre genre = new()
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            Genre genre2 = new()
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(genre);
            await handler.Handle(new CreateGenreCommand(), default);
            A.CallTo(() => repository.AddAsync(genre2)).MustNotHaveHappened();
        }
    }
}
