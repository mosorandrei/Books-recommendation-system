
using System.Threading.Tasks;
using Application.Features.Queries;
using FakeItEasy;
using MediatR;
using WebAPI.Controllers.v1;
using Xunit;

namespace Infrastructure.Test.API.v1
{
    public class GenresControllerTests
    {
        private readonly GenresController controller;
        private readonly IMediator mediator;

        public GenresControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new GenresController(mediator);
        }

        [Fact]
        public async Task Given_GenresController_When_GetIsCalled_Then_ShouldReturnAGenreCollection()
        {
            await controller.Get();
            A.CallTo(() => mediator.Send(A<GetGenresQuery>._, default)).MustHaveHappenedOnceExactly();

        }

    }
}
