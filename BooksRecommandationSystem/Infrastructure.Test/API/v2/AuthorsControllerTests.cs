
using System.Threading.Tasks;
using Application.Features.Queries;
using FakeItEasy;
using MediatR;
using WebAPI.Controllers.v2;
using Xunit;

namespace Infrastructure.Test.API.v2
{
    public class AuthorsControllerTests
    {
        private readonly AuthorsController controller;
        private readonly IMediator mediator;

        public AuthorsControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new AuthorsController(mediator);
        }

        [Fact]
        public async Task Given_AuthorsController_When_GetIsCalled_Then_ShouldReturnAnAuthorCollection()
        {
            await controller.Get();
            A.CallTo(() => mediator.Send(A<GetAuthorsQuery>._, default)).MustHaveHappenedOnceExactly();

        }

    }
}
