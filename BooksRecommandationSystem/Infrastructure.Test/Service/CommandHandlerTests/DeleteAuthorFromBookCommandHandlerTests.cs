

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
    public class DeleteAuthorFromBookCommandHandlerTests
    {
        private readonly DeleteAuthorFromBookCommandHandler handler;
        private readonly IBookAuthorAssociationRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public DeleteAuthorFromBookCommandHandlerTests()
        {
            repository = A.Fake<IBookAuthorAssociationRepository>();
            bookRepository = A.Fake<IBookRepository>();
            authorRepository = A.Fake<IAuthorRepository>();
            handler = new DeleteAuthorFromBookCommandHandler(repository, bookRepository, authorRepository);
        }
        [Fact]
        public async Task GivenDeleteAuthorFromBookCommandHandlerWhenHandleIsCalledThenDeleteAsyncAuthorIsCalled()
        {
            DeleteAuthorFromBookCommand command = new DeleteAuthorFromBookCommand
            {
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                AuthorId = new Guid("6a55c0c8-a75c-4a4a-9714-5a31e431e052")
            };
            BookAuthorAssociation association = new BookAuthorAssociation
            {
                AuthorId = new Guid("6a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")

            };
            IEnumerable<BookAuthorAssociation> associations = new[]
            {
                association
            };
            A.CallTo(() => repository.GetAllAsync()).Returns(associations);

            string result = await handler.Handle(command, default);

            result.Should().Be("Author no longer associated with book!");
        }
        [Fact]
        public void GivenDeleteAuthorFromBookCommandHandlerWhenHandleIsCalledAndBookIdIsNullThenShouldThrowArgumentNullException()
        {
            DeleteAuthorFromBookCommand command = new DeleteAuthorFromBookCommand
            {
                BookId = Guid.Empty,
                AuthorId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052")
            };

            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void GivenDeleteAuthorFromBookCommandHandlerWhenHandleIsCalledAndAuthorIdIsNullThenShouldThrowArgumentNullException()
        {
            DeleteAuthorFromBookCommand command = new DeleteAuthorFromBookCommand
            {
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                AuthorId = Guid.Empty
            };

            Func<Task> action = async () => await handler.Handle(command, default);

            _ = action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenDeleteAuthorFromBookCommandHandlerWhenHandleIsCalledAndAuthorIsNotAssociatedThenSHouldReturnMessage()
        {
            DeleteAuthorFromBookCommand command = new DeleteAuthorFromBookCommand
            {
                BookId = new Guid("8a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                AuthorId = new Guid("6a55c0c8-a75c-4a4a-9714-5a31e431e052")
            };
            BookAuthorAssociation association = new BookAuthorAssociation
            {
                AuthorId = new Guid("1a55c0c8-a75c-4a4a-9714-5a31e431e052"),
                BookId = new Guid("1a55c0c8-a75c-4a4a-9714-5a31e431e052")

            };
            IEnumerable<BookAuthorAssociation> associations = new[]
            {
                association
            };
            A.CallTo(() => repository.GetAllAsync()).Returns(associations);

            string result = await handler.Handle(command, default);

            result.Should().Be("Author is not associated with book!");
        }
    }
}
