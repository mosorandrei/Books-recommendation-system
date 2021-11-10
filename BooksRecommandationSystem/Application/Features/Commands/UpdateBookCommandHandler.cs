﻿using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Guid>
    {
        private readonly IBookRepository repository;

        public UpdateBookCommandHandler(IBookRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Guid> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = repository.GetByIdAsync(request.Id).Result;
            if (book == null || book.Id == Guid.Empty)
            {
                throw new Exception("Book does not exist!");
            }
            book.Rating = request.Rating;
            book.Description = request.Description;
            book.Title = request.Title;
            book.PublicationDate = request.PublicationDate;
            book.ImageUri = request.ImageUri;

            await repository.UpdateAsync(book);

            return book.Id;
        }
    }
}