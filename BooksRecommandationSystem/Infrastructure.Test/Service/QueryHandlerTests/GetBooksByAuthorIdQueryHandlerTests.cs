

using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetBooksByAuthorIdQueryHandlerTests
    {
        private readonly GetBooksByAuthorIdQueryHandler handler;
        private readonly IBookGenreAssociationRepository genreAssociationRepository;
        private readonly IGenreRepository genreRepository;
        private readonly IBookRepository bookRepository;
        private readonly IBookAuthorAssociationRepository repository;
        private readonly IAuthorRepository authorRepository;

        public GetBooksByAuthorIdQueryHandlerTests()
        {
            genreAssociationRepository = A.Fake<IBookGenreAssociationRepository>();
            genreRepository = A.Fake<IGenreRepository>();
            bookRepository = A.Fake<IBookRepository>();
            repository = A.Fake<IBookAuthorAssociationRepository>();
            authorRepository = A.Fake<IAuthorRepository>();
            handler = new GetBooksByAuthorIdQueryHandler(repository, bookRepository, genreAssociationRepository, authorRepository, genreRepository);
        }

        [Fact]
        public async Task GivenGetBooksByAuthorIdQueryHandlerWhenHandleIsCalledThenGetAllAsyncIsCalled()
        {
            
               await handler.Handle(new GetBooksByAuthorIdQuery(), default);
            _ = A.CallTo(() => repository.GetBooksByAuthorId(A<Guid>._)).MustHaveHappenedOnceExactly();
        }
    }
}
