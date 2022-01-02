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
            repository = A.Fake<ITokenRepository>();
            httpContextAccessor = A.Fake<IHttpContextAccessor>();
            handler = new AuthenticateCommandHandler(this.repository, this.httpContextAccessor);
        }

        [Fact]
        public async void GivenAuthenticateCommandWhenhandleIsCalledThenAuthenticateTheUser()
        {
            AuthenticateCommand command = new AuthenticateCommand();
            command.Email = "mosorandrei49@gmail.com";
            command.Password = "parola";
            string ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            ipAddress = "not null";
            await handler.Handle(command, default);
            A.CallTo(() => repository.Authenticate(command, ipAddress)).MustHaveHappenedOnceExactly();
        }
    }
}
