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
    public class GetBookByIdQueryHandlerTests
    {
        private readonly GetBookByIdQueryHandler handler;
        private readonly IBookRepository repository;
        private readonly IBookAuthorAssociationRepository authorAssociationRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookGenreAssociationRepository genreAssociationRepository;
        private readonly IGenreRepository genreRepository;

        public GetBookByIdQueryHandlerTests()
        {
            repository = A.Fake<IBookRepository>();
            authorAssociationRepository = A.Fake<IBookAuthorAssociationRepository>();
            authorRepository = A.Fake<IAuthorRepository>();
            genreAssociationRepository = A.Fake<IBookGenreAssociationRepository>();
            genreRepository = A.Fake<IGenreRepository>();
            handler = new GetBookByIdQueryHandler(repository,authorAssociationRepository,genreAssociationRepository,authorRepository,genreRepository);
        }

        [Fact]
        public async Task GivenGetBookByIdQueryHandlerWhenHandleIsCalledThenGetByIdAsyncIsCalled()
        {
            await handler.Handle(new GetBookByIdQuery(), default);
            A.CallTo(() => repository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GivenGetBookByIdQueryHandlerWhenHandleIsCalledAndBookDoesNotExistThenShouldThrowException()
        {
            Book? book = null;

            A.CallTo(() => repository.GetByIdAsync(new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"))).Returns(book);

            Func<Task> action = async () => await handler.Handle(new GetBookByIdQuery
            {
                Id = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
            }, default);

            _ = action.Should().ThrowAsync<Exception>(); ;
        }
    }
}
