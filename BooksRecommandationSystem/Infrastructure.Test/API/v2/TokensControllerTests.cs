using Application.Features.Commands;
using Application.Features.Queries;
using FakeItEasy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers.v2;
using Xunit;

namespace Infrastructure.Test.API.v2
{
    public class TokensControllerTests
    {
        private readonly TokenController controller;
        private readonly IMediator mediator;
        private readonly AuthenticateCommand AuthCommand;
        private readonly RegisterMemberCommand RegMemberCommand;

        public TokensControllerTests()
        {
            mediator = A.Fake<IMediator>();
            AuthCommand = A.Fake<AuthenticateCommand>();
            RegMemberCommand = A.Fake<RegisterMemberCommand>();
            controller = new TokenController(mediator);
        }


        [Fact]
        public async Task GivenTokensControllerWhenAuthenticateAsyncisCalledThenAuthenticateTheUser()
        {
            await controller.AuthenticateAsync(AuthCommand);
            A.CallTo(() => mediator.Send(A<AuthenticateCommand>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GivenTokensControllerWhenRegisterAsyncisCalledThenRegisterTheUser()
        {
            await controller.RegisterAsync(RegMemberCommand);
            A.CallTo(() => mediator.Send(A<RegisterMemberCommand>._, default)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task GivenTokensControllerWhenGetAdminsIsCalledThenShouldReturnAnAdminsCollection()
        {
            await controller.GetAdmins();
            A.CallTo(() => mediator.Send(A<GetAdminsQuery>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GivenTokensControllerWhenGetMembersIsCalledThenShouldReturnAMembersCollection()
        {
            await controller.GetMembers();
            A.CallTo(() => mediator.Send(A<GetMembersQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}
