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

        public TokensControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new TokenController(mediator);
        }

        [Fact]
        public async Task Given_TokensController_When_GetIsCalled_Then_ShouldReturnAnAdminsCollection()
        {
            await controller.GetAdmins();
            A.CallTo(() => mediator.Send(A<GetAdminsQuery>._, default)).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async Task Given_TokensController_When_GetIsCalled_Then_ShouldReturnAMembersCollection()
        {
            await controller.GetMembers();
            A.CallTo(() => mediator.Send(A<GetMembersQuery>._, default)).MustHaveHappenedOnceExactly();

        }
    }
}
