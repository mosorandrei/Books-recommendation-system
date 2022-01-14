using Application.Interfaces;
using Domain.AuthModels;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Application.Features.Commands
{
    public class SendResetEmailCommandHandler : IRequestHandler<SendResetEmailCommand, string>
    {
        private readonly IResetPasswordRepository repository;
        private readonly ITokenRepository tokenRepository;

        public SendResetEmailCommandHandler(IResetPasswordRepository repository, ITokenRepository tokenRepository)
        {
            this.repository = repository;
            this.tokenRepository = tokenRepository;
        }

        public async Task<string> Handle(SendResetEmailCommand request, CancellationToken cancellationToken)
        {
            if (request.Email == null)
                throw new ArgumentNullException("Email is null in reset password!");
            var code = generateRandomResetCode();
            if (!await PersistToDatabase(request.Email, code))
                throw new Exception("Email not found in Database!");
            var apiKey = ResetPasswordConstants.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("BooksRecommendationSystem@gmail.com", "BRS Team");
            var subject = "BooksRecommendationSystem -  Forgot Password";
            var to = new EmailAddress(request.Email, "BNR User");
            var plainTextContent = "Your reset code in order to reset your password is: " + code;
            var htmlContent = "<strong>" + plainTextContent + "</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg, cancellationToken);
#pragma warning disable CS8603 // Possible null reference return.
            return response.ToString();
#pragma warning restore CS8603 // Possible null reference return.
        }

        private int generateRandomResetCode()
        {
            Random generator = new();
            return generator.Next(100000, 1000000);
        }
        private async Task<bool> PersistToDatabase(string email, int code)
        {
            bool emailFound = false;
            IEnumerable<ApplicationUserDto> allMembers = await tokenRepository.GetAllMembersAsync();
            foreach (ApplicationUserDto member in allMembers)
            {
                if (member.Email == email)
                {
                    emailFound = true;
                }
            }
            IEnumerable<ApplicationUserDto> allAdmins = await tokenRepository.GetAllAdminsAsync();
            foreach (ApplicationUserDto admin in allAdmins)
            {
                if (admin.Email == email)
                {
                    emailFound = true;
                }
            }
            if (!emailFound)
                return false;
            IEnumerable<ResetPasswordToken> allTokens = await repository.GetAllAsync();
            foreach (ResetPasswordToken token in allTokens)
            {
                if (token.Email == email)
                    await repository.DeleteAsync(token);
            }
            await repository.AddAsync(new ResetPasswordToken()
            {
                Email = email,
                Token = code
            });
            return true;
        }
    }
}
