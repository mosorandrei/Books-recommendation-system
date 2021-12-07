using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Domain.AuthModels
{
    public class TokenResponse
    {
        public TokenResponse(ApplicationUser user,
                             string role,
                             string token
                            )
        {
            Id = user.Id;
            FullName = user.FullName;
            EmailAddress = user.Email;
            Token = token;
            ExpireTime = (long)(new JwtSecurityTokenHandler().ReadJwtToken(Token).ValidTo - DateTime.UtcNow).TotalSeconds;
            Role = role;
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public long ExpireTime { get; set; }
        public string Role { get; set; }
    }
}
