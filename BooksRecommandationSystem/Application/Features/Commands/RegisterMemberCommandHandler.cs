using Application.Features.Responses;
using Application.Interfaces;
using Domain.AuthModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands
{
    public class RegisterMemberCommandHandler : IRequestHandler<RegisterMemberCommand, RegistrationResponse>
    {
        private readonly ITokenRepository repository;
        private readonly HttpContext _httpContext;

        public RegisterMemberCommandHandler(ITokenRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._httpContext = (httpContextAccessor is not null) ? httpContextAccessor.HttpContext : throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public async Task<RegistrationResponse> Handle(RegisterMemberCommand command, CancellationToken cancellationToken)
        {
            RegistrationResponse response = new();

            RegisterResponse registerResponse = await repository.RegisterMember(command);
            response.Resource = registerResponse ?? throw new NullReferenceException();
            return response;
        }
    }
}
