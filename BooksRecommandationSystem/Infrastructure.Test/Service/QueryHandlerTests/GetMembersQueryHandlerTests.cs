using System.Threading.Tasks;
using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetMembersQueryHandlerTests
    {
        private readonly GetMembersQueryHandler handler;
        private readonly ITokenRepository repository;

        public GetMembersQueryHandlerTests()
        {
            this.repository = A.Fake<ITokenRepository>();
            this.handler = new GetMembersQueryHandler(this.repository);
        }

        [Fact]
        public async Task GivenGetMembersQueryHandlerWhenHandleIsCalledThenGetAllMembersAsyncIsCalled()
        {
            await handler.Handle(new GetMembersQuery(), default);
            A.CallTo(() => repository.GetAllMembersAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
