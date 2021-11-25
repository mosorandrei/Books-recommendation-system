using Application.Features.Responses;
using Application.Interfaces;
using Domain.AuthModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticationResponse>
    {
        private readonly ITokenRepository repository;
        private readonly HttpContext _httpContext;

        public AuthenticateCommandHandler(ITokenRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._httpContext = (httpContextAccessor != null) ? httpContextAccessor.HttpContext : throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public async Task<AuthenticationResponse> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
        {
            AuthenticationResponse response = new AuthenticationResponse();

            string ipAddress = _httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            TokenResponse tokenResponse = await repository.Authenticate(command, ipAddress);
            if (tokenResponse == null)
            {
                throw new NullReferenceException();
            }

            response.Resource = tokenResponse;
            return response;
        }
    }
}
