using Application.Features.Queries;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetGenreByIdQueryHandlerTests
    {
        private readonly GetGenreByIdQueryHandler handler;
        private readonly IGenreRepository repository;

        public GetGenreByIdQueryHandlerTests()
        {
            this.repository = A.Fake<IGenreRepository>();
            this.handler = new GetGenreByIdQueryHandler(this.repository);
        }

        [Fact]
        public async Task GivenGetGenreByIdQueryHandlerWhenHandleIsCalledThenGetByIdAsyncIsCalled()
        {
            await handler.Handle(new GetGenreByIdQuery(), default);
            A.CallTo(() => repository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GivenGetGenreByIdQueryHandlerWhenHandleIsCalledAndGenreDoesNotExistThenShouldThrowException()
        {
            Genre? genre = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"))).Returns(genre);

            Func<Task> action = async () => await handler.Handle(new GetGenreByIdQuery
            {
                Id = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
            }, default);

            _ = action.Should().ThrowAsync<Exception>(); ;
        }
    }
}
