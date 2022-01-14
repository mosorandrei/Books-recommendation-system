using MediatR;

namespace Application.Features.Commands
{
    public class SendResetEmailCommand : IRequest<string>
    {
        public SendResetEmailCommand(string email)
        {
            Email = email;
        }

        public string? Email { get; set; }
    }
}
