using Domain.AuthModels;
using Domain.Entities;

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
        /// <summary>
        ///     Register a user in database as Member
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RegisterResponse> RegisterMember(RegisterRequest request);
        /// <summary>
        ///     Get all Members from Database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ApplicationUserDto>> GetAllMembersAsync();
        /// <summary>
        ///     Get all Admins from Database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ApplicationUserDto>> GetAllAdminsAsync();
        /// <summary>
        ///     Refresh a token based on User Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<RefreshTokenDto> RefreshToken(string Email);
        /// <summary>
        ///     Get a User by Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUserById(string UserId);
        Task<string> BlockUser(string UserId);
        Task<string> UnblockUser(string UserId);
    }
}
