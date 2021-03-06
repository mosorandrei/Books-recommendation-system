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
    }
}
