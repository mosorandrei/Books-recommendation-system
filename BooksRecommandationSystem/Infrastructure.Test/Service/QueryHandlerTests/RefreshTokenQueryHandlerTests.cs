using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class RefreshTokenQueryHandlerTests
    {
        private readonly RefreshTokenQueryHandler handler;
        private readonly ITokenRepository repository;

        public RefreshTokenQueryHandlerTests()
        {
            repository = A.Fake<ITokenRepository>();
            handler = new RefreshTokenQueryHandler(repository);
        }

        [Fact]
        public async Task GivenStatusesQueryWhenHandleIsCalledThenGetAllAsyncIsCalled()
        {
            RefreshTokenQuery query = new RefreshTokenQuery("mosorandrei49@gmail.com");
            await handler.Handle(query, default);
            A.CallTo(() => repository.RefreshToken(A<string>._)).MustHaveHappenedOnceExactly();

        }




    }
}
