using System;
using System.Threading.Tasks;
using Application.Features.Commands;
using Application.Interfaces;
using Domain.AuthModels;
using FakeItEasy;
using FluentAssertions;
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
          handler = new AuthenticateCommandHandler(this.repository,this.httpContextAccessor);
        }

        [Fact]
        public async Task GivenAuthenticateCommandWhenHandleIsCalledThenShouldAuthenticateTheUser()
        {    
             await handler.Handle(new AuthenticateCommand()
             {
                 Email = "mosorandrei49@gmail.com",
                 Password = "Parola123!"
             }, default);
            _ = A.CallTo(() => repository.Authenticate(A<AuthenticateCommand>._, A<string>._)).MustHaveHappenedOnceExactly();
            
        }
    }
}
