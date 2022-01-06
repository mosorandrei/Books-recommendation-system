using Application.Features.Queries;
using Application.Interfaces;
using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.QueryHandlerTests
{
    public class GetBooksQueryHandlerTests
    {
        private readonly GetBooksQueryHandler handler;
        private readonly IBookRepository repository;
        private readonly IBookAuthorAssociationRepository authorAssociationRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookGenreAssociationRepository genreAssociationRepository;
        private readonly IGenreRepository genreRepository;

        public GetBooksQueryHandlerTests()
        {
            repository = A.Fake<IBookRepository>();
            authorAssociationRepository = A.Fake<IBookAuthorAssociationRepository>();
            authorRepository = A.Fake<IAuthorRepository>();
            genreAssociationRepository = A.Fake<IBookGenreAssociationRepository>();
            genreRepository = A.Fake<IGenreRepository>();
            handler = new GetBooksQueryHandler(repository, authorAssociationRepository, genreAssociationRepository, authorRepository, genreRepository);
        }

        [Fact]
        public async Task GivenGetBooksQueryHandlerWhenHandleIsCalledThenGetAllAsyncIsCalled()
        {
            await handler.Handle(new GetBooksQuery(), default);
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();
        }
    }
}