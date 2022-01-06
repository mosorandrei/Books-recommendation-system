using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service
{
    public class CreateAuthorCommandHandlerTests
    {
        private readonly CreateAuthorCommandHandler handler;
        private readonly IAuthorRepository repository;

        public CreateAuthorCommandHandlerTests()
        {
            this.repository = A.Fake<IAuthorRepository>();
            this.handler = new CreateAuthorCommandHandler(this.repository);
        }

        [Fact]
        public async Task GivenCreateAuthorCommandHandlerWhenHandleIsCalledThenAddAsyncAuthorIsCalled()
        {
            await handler.Handle(new CreateAuthorCommand(), default);
            A.CallTo(() => repository.AddAsync(A<Author>._)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task GivenCreateAuthorCommandHandlerWhenHandleIsCalledAndGuidExistsThenAddAsyncAuthorIsNotCalled()
        {
            Author author = new()
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            Author author2 = new()
            {
                Id = new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba")
            };

            A.CallTo(() => repository.GetByIdAsync(new Guid("25bb7416-4cbb-4b46-931b-604199ae6cba"))).Returns(author);
            await handler.Handle(new CreateAuthorCommand(), default);
            A.CallTo(() => repository.AddAsync(author2)).MustNotHaveHappened();
        }
    }
}
