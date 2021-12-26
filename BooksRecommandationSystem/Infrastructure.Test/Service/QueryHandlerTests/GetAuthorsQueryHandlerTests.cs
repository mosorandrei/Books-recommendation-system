using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetAuthorsQueryHandlerTests
    {
        private readonly GetAuthorsQueryHandler handler;
        private readonly IAuthorRepository repository;

        public GetAuthorsQueryHandlerTests()
        {
            this.repository = A.Fake<IAuthorRepository>();
            this.handler = new GetAuthorsQueryHandler(this.repository);
        }

        [Fact]
        public async Task GivenGetAuthorsQueryHandlerWhenHandleIsCalledThenGetAllAsyncIsCalled()
        {
            await handler.Handle(new GetAuthorsQuery(), default);
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
