using Application.Features.Responses;
using Application.Interfaces;
using Domain.AuthModels;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands
{
    public class RegisterMemberCommandHandler : IRequestHandler<RegisterMemberCommand, RegistrationResponse>
    {
        private readonly ITokenRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IReadingStatusRepository statusRepository;
        private readonly HttpContext _httpContext;

        public RegisterMemberCommandHandler(ITokenRepository repository, IHttpContextAccessor httpContextAccessor, IBookRepository bookRepository, IReadingStatusRepository statusRepository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._httpContext = (httpContextAccessor is not null) ? httpContextAccessor.HttpContext : throw new ArgumentNullException(nameof(httpContextAccessor));
            this.bookRepository = bookRepository;
            this.statusRepository = statusRepository;
        }
        public async Task<RegistrationResponse> Handle(RegisterMemberCommand command, CancellationToken cancellationToken)
        {
            RegistrationResponse response = new();

            RegisterResponse registerResponse = await repository.RegisterMember(command);
            // Response ID will never be null, only when running tests. Thus, suppress here.
#pragma warning disable CS8604 // Possible null reference argument.
            AddUserToStatuses(registerResponse.Id); 
#pragma warning restore CS8604 // Possible null reference argument.
            response.Resource = registerResponse ?? throw new NullReferenceException();
            return response;
        }
        private async void AddUserToStatuses(string currentUserId)
        {
            IEnumerable<Book> allBooks = await bookRepository.GetAllAsync();
            foreach (var book in allBooks)
            {
                await statusRepository.AddAsync(new ReadingStatus
                {
                    ApplicationUserId = currentUserId,
                    BookId = book.Id,
                    Status = ReadingStatusEnum.ToBeReaded,
                    UserScore = 0
                });
            }
        }
    }
}
