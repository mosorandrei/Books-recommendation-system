
using System.Threading.Tasks;
using Application.Features.Queries;
using FakeItEasy;
using MediatR;
using WebAPI.Controllers.v1;
using Xunit;

namespace Infrastructure.Test.API.v1
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
        public async Task GivenAuthorsControllerWhenGetIsCalledThenShouldReturnAnAuthorCollection()
        {
            await controller.Get();
            A.CallTo(() => mediator.Send(A<GetAuthorsQuery>._, default)).MustHaveHappenedOnceExactly();
        }

    }
}
