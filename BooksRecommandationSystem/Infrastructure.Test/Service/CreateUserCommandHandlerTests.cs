using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service
{
    public class CreateUserCommandHandlerTests
    {
        private readonly CreateUserCommandHandler handler;
        private readonly IUserRepository repository;

        public CreateUserCommandHandlerTests()
        {
            this.repository = A.Fake<IUserRepository>();
            this.handler = new CreateUserCommandHandler(this.repository);
        }

        [Fact]
        public async Task Given_CreateUserCommandHandler_When_HandleIsCalled_Then_AddAsyncUserIsCalled()
        {
            await handler.Handle(new CreateUserCommand(), default);
            A.CallTo(() => repository.AddAsync(A<User>._)).MustHaveHappenedOnceExactly();

        }
    }
}
