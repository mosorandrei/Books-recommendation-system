using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands
{
    public class ResetPasswordCommand : IRequest<IdentityResult>
    {
        public ResetPasswordCommand(string email, int token, string password, string confirmPassword)
        {
            Email = email;
            Token = token;
            Password = password;
            this.ConfirmPassword = confirmPassword;
        }

        public string? Email { get; set; }
        public int Token { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
