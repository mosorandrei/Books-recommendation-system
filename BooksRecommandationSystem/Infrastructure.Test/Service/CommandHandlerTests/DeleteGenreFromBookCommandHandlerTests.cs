

using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.Service.CommandHandlerTests
{
    public class DeleteGenreFromBookCommandHandlerTests
    {
        private readonly DeleteGenreFromBookCommandHandler handler;
        private readonly IBookGenreAssociationRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IGenreRepository genreRepository;

        public DeleteGenreFromBookCommandHandlerTests()
        {
            repository = A.Fake<IBookGenreAssociationRepository>();
            bookRepository = A.Fake<IBookRepository>();
            genreRepository = A.Fake<IGenreRepository>();
            handler = new DeleteGenreFromBookCommandHandler(repository, bookRepository, genreRepository);
        }
        [Fact]
        public async Task GivenDeleteGenreFromBookCommandHandlerWhenHandleIsCalledThenDeleteAsyncGenreIsCalled()
        {
            DeleteGenreFromBookCommand command = new DeleteGenreFromBookCommand
            {
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                GenreId = new Guid("6a55c0c8-a75c-4a4a-9714-5a31e431e052")
            };
            BookGenreAssociation association = new BookGenreAssociation
            {
                GenreId = new Guid("6a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")

            };
            IEnumerable<BookGenreAssociation> associations = new[]
            {
                association
            };
            A.CallTo(() => repository.GetAllAsync()).Returns(associations);

            string result = await handler.Handle(command, default);

            result.Should().Be("Genre no longer associated with book!");
        }
        [Fact]
        public void GivenDeleteGenreFromBookCommandHandlerWhenHandleIsCalledAndBookIdIsNullThenShouldThrowArgumentNullException()
        {
            DeleteGenreFromBookCommand command = new DeleteGenreFromBookCommand
            {
                BookId = Guid.Empty,
                GenreId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
            };

            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void GivenDeleteGenreFromBookCommandHandlerWhenHandleIsCalledAndGenreIdIsNullThenShouldThrowArgumentNullException()
        {
            DeleteGenreFromBookCommand command = new DeleteGenreFromBookCommand
            {
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                GenreId = Guid.Empty
            };

            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenDeleteGenreFromBookCommandHandlerWhenHandleIsCalledAndGenreIsNotAssociatedThenSHouldReturnMessage()
        {
            DeleteGenreFromBookCommand command = new DeleteGenreFromBookCommand
            {
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                GenreId = new Guid("6a55c0c8-a75c-4a4a-9714-5a31e431e052")
            };
            BookGenreAssociation association = new BookGenreAssociation
            {
                GenreId = new Guid("1a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                BookId = new Guid("1a55c0c8-a75c-4a4a-9714-5a31e431e052")

            };
            IEnumerable<BookGenreAssociation> associations = new[]
            {
                association
            };
            A.CallTo(() => repository.GetAllAsync()).Returns(associations);

            string result = await handler.Handle(command, default);

            result.Should().Be("Genre is not associated with book!");
        }
    }
}
