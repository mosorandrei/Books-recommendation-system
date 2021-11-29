

using Application.Features.Commands;
using Application.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Infrastructure.Test.Service
{
    public class AuthenticateCommandHandlerTests
    {
        private readonly AuthenticateCommandHandler handler;
        private readonly ITokenRepository repository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticateCommandHandlerTests()
        {
          this.repository = A.Fake<ITokenRepository>();
          this.httpContextAccessor = A.Fake<IHttpContextAccessor>();
          this.handler = new AuthenticateCommandHandler(this.repository,this.httpContextAccessor);
        }

        [Fact]
        public async void Given_AuthenticateCommand_When_HandleIsCalled_Then_ShouldAuthenticateTheUser()
        {
            await handler.Handle(new AuthenticateCommand(), default);
            A.CallTo(() => repository.Authenticate(A<AuthenticateCommand>._,A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
