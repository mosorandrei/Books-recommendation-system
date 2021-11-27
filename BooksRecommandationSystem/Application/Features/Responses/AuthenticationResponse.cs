using Domain.AuthModels;

namespace Application.Features.Responses
{
    public class AuthenticationResponse
    {
        public TokenResponse? Resource { get; set; }
    }
}
