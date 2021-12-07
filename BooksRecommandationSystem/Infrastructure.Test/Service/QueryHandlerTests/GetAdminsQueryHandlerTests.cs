using Application.Features.Queries;
using Application.Interfaces;
using Domain.AuthModels;
using FakeItEasy;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetAdminsQueryHandlerTests
    {
        private readonly GetAdminsQueryHandler handler;
        private readonly ITokenRepository repository;

        public GetAdminsQueryHandlerTests()
        {
            this.repository = A.Fake<ITokenRepository>();
            this.handler = new GetAdminsQueryHandler(this.repository);
        }

        [Fact]
        public async Task GivenGetAdminsQueryHandlerWhenHandleIsCalledThenGetAllAdminsAsyncIsCalled()
        {
            await handler.Handle(new GetAdminsQuery(), default);
            A.CallTo(() => repository.GetAllAdminsAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
