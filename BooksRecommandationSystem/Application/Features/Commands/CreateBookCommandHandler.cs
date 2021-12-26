using Application.Interfaces;
using Domain.AuthModels;
using Domain.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IBookRepository repository;
        private readonly ITokenRepository userRepository;
        private readonly IReadingStatusRepository statusRepository;

        public CreateBookCommandHandler(IBookRepository repository, IReadingStatusRepository statusRepository, ITokenRepository userRepository)
        {
            this.repository = repository;
            this.statusRepository = statusRepository;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Rating = request.Rating,
                Description = request.Description,
                PublicationDate = request.PublicationDate,
                UploadDate = request.UploadDate,
                ImageUri = request.ImageUri,
                DownloadUri = request.DownloadUri
            };

            await repository.AddAsync(book);
            AddBookToStatuses(book.Id);
            return book.Id;
        }

        private async void AddBookToStatuses(Guid currentBookId)
        {
            IEnumerable<ApplicationUserDto> allMembersDTO = await userRepository.GetAllMembersAsync();
            IEnumerable<ApplicationUserDto> allAdminsDTO = await userRepository.GetAllAdminsAsync();
            var allUsersDto = allAdminsDTO.Concat(allMembersDTO);
            foreach (var userDto in allUsersDto)
            {
                await statusRepository.AddAsync(new ReadingStatus
                {
                    ApplicationUserId = userDto.Id,
                    BookId = currentBookId,
                    Status = ReadingStatusEnum.ToBeReaded
                });
            }
        }
    }
}
