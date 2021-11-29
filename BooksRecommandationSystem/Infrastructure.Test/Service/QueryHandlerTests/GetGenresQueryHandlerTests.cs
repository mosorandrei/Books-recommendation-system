using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetGenresQueryHandlerTests
    {
        private readonly GetGenresQueryHandler handler;
        private readonly IGenreRepository repository;

        public GetGenresQueryHandlerTests()
        {
            this.repository = A.Fake<IGenreRepository>();
            this.handler = new GetGenresQueryHandler(this.repository);
        }

        [Fact]
        public async void GivenGetGenresQueryHandler_WhenHandleIsCalled_ThenGetAllAsyncIsCalled()
        {
            await handler.Handle(new GetGenresQuery(), default);
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
