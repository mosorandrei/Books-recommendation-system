using System.Threading.Tasks;
using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetBooksQueryHandlerTests
    {
        private readonly GetBooksQueryHandler handler;
        private readonly IBookRepository repository;

        public GetBooksQueryHandlerTests()
        {
            this.repository = A.Fake<IBookRepository>();
            this.handler = new GetBooksQueryHandler(this.repository);
        }

        [Fact]
        public async Task GivenGetBooksQueryHandlerWhenHandleIsCalledThenGetAllAsyncIsCalled()
        {
            await handler.Handle(new GetBooksQuery(), default);
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
