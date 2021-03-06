
using Application.Features.Queries;
using FakeItEasy;
using MediatR;
using System.Threading.Tasks;
using WebAPI.Controllers.v1;
using Xunit;

namespace Infrastructure.Test.API.v1
{
    public class BooksControllerTests
    {
        private readonly BooksController controller;
        private readonly IMediator mediator;

        public BooksControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new BooksController(mediator);
        }

        [Fact]
        public async Task GivenBooksControllerWhenGetIsCalledThenShouldReturnABookCollection()
        {
            await controller.Get();
            A.CallTo(() => mediator.Send(A<GetBooksQuery>._, default)).MustHaveHappenedOnceExactly();

        }

    }
}
