using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IdentityResult>
    {
        private readonly IResetPasswordRepository repository;
        private readonly ITokenRepository tokenRepository;

        public ResetPasswordCommandHandler(IResetPasswordRepository repository, ITokenRepository tokenRepository)
        {
            this.repository = repository;
            this.tokenRepository = tokenRepository;
        }

        public async Task<IdentityResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.Email == null || request.Password == null || request.ConfirmPassword == null)
                throw new ArgumentNullException("ResetPasswordRequest is not valid!");
            if (request.Password != request.ConfirmPassword)
                throw new ArgumentException("Password does not match the Confirmed Password!");
            IEnumerable<ResetPasswordToken> allTokens = await repository.GetAllAsync();
            foreach (ResetPasswordToken token in allTokens)
            {
                if (token.Email == request.Email)
                {
                    if (token.Token != request.Token)
                        throw new ArgumentException("Inputted code does not match the code given on Email!");
                    else
                    {
                        await repository.DeleteAsync(token);
                        return await tokenRepository.ResetUserPassword(request.Email, request.Password);
                    }
                }
            }
            throw new ArgumentException("Email not found in Reset Password Database!");
        }
    }
}
