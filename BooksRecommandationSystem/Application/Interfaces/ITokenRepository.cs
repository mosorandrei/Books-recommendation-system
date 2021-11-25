using Domain.AuthModels;

namespace Application.Interfaces
{
    public interface ITokenRepository
    {
        /// <summary>
        ///     Validate the credentials entered when logging in.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress);
    }
}
