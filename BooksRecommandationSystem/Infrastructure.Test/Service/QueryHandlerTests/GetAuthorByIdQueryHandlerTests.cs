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
    public class GetAuthorByIdQueryHandlerTests
    {
        private readonly GetAuthorByIdQueryHandler handler;
        private readonly IAuthorRepository repository;

        public GetAuthorByIdQueryHandlerTests()
        {
            this.repository = A.Fake<IAuthorRepository>();
            this.handler = new GetAuthorByIdQueryHandler(this.repository);
        }

        [Fact]
        public async Task GivenGetAuthorByIdQueryHandlerWhenHandleIsCalledThenGetByIdAsyncIsCalled()
        {
            await handler.Handle(new GetAuthorByIdQuery(), default);
            A.CallTo(() => repository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GivenGetAuthorByIdQueryHandlerWhenHandleIsCalledAndAuthorDoesNotExistThenShouldThrowException()
        {
            Author? author = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"))).Returns(author);

            Func<Task> action = async () => await handler.Handle(new GetAuthorByIdQuery
            {
                Id = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
            }, default);

            _ = action.Should().ThrowAsync<Exception>(); ;
        }
    }
}
