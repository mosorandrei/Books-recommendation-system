using Application.Interfaces;
using Domain.AuthModels;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistence.v2
{
    public class TokenRepository : ITokenRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Token _token;
        private readonly HttpContext _httpContext;

        public TokenRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<Token> tokenOptions,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = tokenOptions.Value;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<TokenResponse?> Authenticate(TokenRequest request, string ipAddress)
        {
            if (await IsValidUser(request.Email, request.Password))
            {
                ApplicationUser user = await GetUserByEmail(request.Email);

                if (user != null && user.IsEnabled)
                {
                    string role = (await _userManager.GetRolesAsync(user))[0];
                    string jwtToken = await GenerateJwtToken(user);

                    await _userManager.UpdateAsync(user);

                    return new TokenResponse(user,
                                             role,
                                             jwtToken
                                             //""//refreshToken.Token
                                             );
                }
            }

            return null;
        }

        public async Task<RegisterResponse?> RegisterMember(RegisterRequest request)
        {
            if (!await IsValidUser(request.Email, request.Password))
            {
                ApplicationUser user = new()
                {
                    Email = request.Email,
                    UserName = request.Username,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IsEnabled = true,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                    return new RegisterResponse { Status = "Error", Message = "User creation failed!" };
                await _userManager.AddToRoleAsync(user, ApplicationIdentityConstants.Roles.Member);
                return new RegisterResponse { Status = "Succes", Message = "User created successfully!" };
            }

            return new RegisterResponse { Status = "Error", Message = "User already exists!" };
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetAllMembersAsync()
        {
            List<ApplicationUser> members = new(await _userManager.GetUsersInRoleAsync(ApplicationIdentityConstants.Roles.Member));
            return members.Select(member => new ApplicationUserDTO()
            {
                Username = member.UserName,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName
            });
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetAllAdminsAsync()
        {
            List<ApplicationUser> admins = new(await _userManager.GetUsersInRoleAsync(ApplicationIdentityConstants.Roles.Administrator));
            return admins.Select(member => new ApplicationUserDTO()
            {
                Username = member.UserName,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName
            });
        }

        private async Task<bool> IsValidUser(string email, string password)
        {
            ApplicationUser user = await GetUserByEmail(email);

            if (user == null)
            {
                // Username or password was incorrect.
                return false;
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);

            return signInResult.Succeeded;
        }

        private async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            string role = (await _userManager.GetRolesAsync(user))[0];
            byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);

            JwtSecurityTokenHandler handler = new();
            SecurityTokenDescriptor descriptor = new()
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id),
                    new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
