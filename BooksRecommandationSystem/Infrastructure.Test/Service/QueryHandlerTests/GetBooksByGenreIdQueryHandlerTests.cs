

using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetBooksByGenreIdQueryHandlerTests
    {
        private readonly GetBooksByGenreIdQueryHandler handler;
        private readonly IBookGenreAssociationRepository repository;
        private readonly IGenreRepository genreRepository;
        private readonly IBookRepository bookRepository;
        private readonly IBookAuthorAssociationRepository bookAuthorAssociationRepository;
        private readonly IAuthorRepository authorRepository;

        public GetBooksByGenreIdQueryHandlerTests()
        {
            bookRepository = A.Fake<IBookRepository>();
            bookAuthorAssociationRepository = A.Fake<IBookAuthorAssociationRepository>();
            authorRepository = A.Fake<IAuthorRepository>();
            genreRepository = A.Fake<IGenreRepository>();
            repository = A.Fake<IBookGenreAssociationRepository>();
            handler = new GetBooksByGenreIdQueryHandler(repository, bookRepository, bookAuthorAssociationRepository, authorRepository, genreRepository);
        }

        [Fact]
        public async Task GivenGetBooksByAuthorIdQueryHandlerWhenHandleIsCalledThenGetAllAsyncIsCalled()
        {

            await handler.Handle(new GetBooksByGenreIdQuery(), default);
            _ = A.CallTo(() => repository.GetBooksByGenreId(A<Guid>._)).MustHaveHappenedOnceExactly();
        }
    }
}
