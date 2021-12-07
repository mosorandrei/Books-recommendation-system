using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
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
    }
}
