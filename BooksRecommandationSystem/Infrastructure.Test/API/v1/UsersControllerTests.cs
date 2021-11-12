
using System.Threading.Tasks;
using Application.Features.Queries;
using FakeItEasy;
using MediatR;
using WebAPI.Controllers.v1;
using Xunit;

namespace Infrastructure.Test.API.v1
{
    public class UsersControllerTests
    {
        private readonly UsersController controller;
        private readonly IMediator mediator;

        public UsersControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new UsersController(mediator);
        }

        [Fact]
        public async Task Given_UsersController_When_GetIsCalled_Then_ShouldReturnAUserCollection()
        {
            await controller.Get();
            A.CallTo(() => mediator.Send(A<GetUsersQuery>._, default)).MustHaveHappenedOnceExactly();

        }

    }
}
