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

        public async Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress)
        {
            if (request.Email is null || request.Password is null)
                throw new ArgumentNullException(nameof(request));


            if (await IsValidUser(request.Email, request.Password))
            {
                ApplicationUser user = await GetUserByEmail(request.Email);

                if (user != null && user.IsEnabled && user.IsBlocked == 0)
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
                else
                {
                    throw new SecurityTokenException("User is null, disabled or blocked!");
                }
            }

            throw new ArgumentNullException(nameof(request));
        }

        public async Task<RegisterResponse> RegisterMember(RegisterRequest request)
        {
            if (request.Email is null || request.Password is null)
                throw new ArgumentNullException(nameof(request));

            if (!await IsValidUser(request.Email, request.Password))
            {
                ApplicationUser user = new()
                {
                    Email = request.Email,
                    UserName = request.Username,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IsEnabled = true,
                    IsBlocked = 0,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                    return new RegisterResponse { Id = "INVALID_USER_ID", Status = "Error", Message = "User creation failed!" };
                await _userManager.AddToRoleAsync(user, ApplicationIdentityConstants.Roles.Member);
                return new RegisterResponse { Id = user.Id, Status = "Succes", Message = "User created successfully!" };
            }

            return new RegisterResponse { Id = "INVALID_USER_ID", Status = "Error", Message = "User already exists!" };
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetAllMembersAsync()
        {
            List<ApplicationUser> members = new(await _userManager.GetUsersInRoleAsync(ApplicationIdentityConstants.Roles.Member));
            return members.Select(member => new ApplicationUserDto()
            {
                Id = member.Id,
                Username = member.UserName,
                Email = member.Email,
                ImageUri = member.ImageUri,
                FirstName = member.FirstName,
                LastName = member.LastName,
                IsBlocked = member.IsBlocked
            });
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetAllAdminsAsync()
        {
            List<ApplicationUser> admins = new(await _userManager.GetUsersInRoleAsync(ApplicationIdentityConstants.Roles.Administrator));
            return admins.Select(member => new ApplicationUserDto()
            {
                Id = member.Id,
                Username = member.UserName,
                Email = member.Email,
                ImageUri = member.ImageUri,
                FirstName = member.FirstName,
                LastName = member.LastName,
                IsBlocked = member.IsBlocked
            });
        }

        public async Task<RefreshTokenDto> RefreshToken(string Email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(Email);

            if (user != null && user.IsEnabled)
            {
                string jwtRefreshToken = await GenerateJwtToken(user);

                await _userManager.UpdateAsync(user);

                return new RefreshTokenDto
                {
                    Token = jwtRefreshToken,
                    ExpireTime = (long)(new JwtSecurityTokenHandler().ReadJwtToken(jwtRefreshToken).ValidTo - DateTime.UtcNow).TotalSeconds
                };
            }

            throw new InvalidDataException("Something went wrong when fetching the User!");
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

        public async Task<ApplicationUser> GetUserById(string UserId)
        {
            return await _userManager.FindByIdAsync(UserId);
        }

        public async Task<string> BlockUser(string UserId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(UserId);
            if (user.IsBlocked == 1)
                return "User already blocked!";
            user.IsBlocked = 1;
            await _userManager.UpdateAsync(user);
            return "User blocked successfully!";
        }

        public async Task<string> UnblockUser(string UserId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(UserId);
            if (user.IsBlocked == 0)
                return "User is not blocked!";
            user.IsBlocked = 0;
            await _userManager.UpdateAsync(user);
            return "User unblocked successfully!";
        }

        public async Task<IdentityResult> ResetUserPassword(string Email, string NewPassword)
        {
            ApplicationUser currentUser = await GetUserByEmail(Email);
            string ResetToken = await _userManager.GeneratePasswordResetTokenAsync(currentUser);
            return await _userManager.ResetPasswordAsync(currentUser, ResetToken, NewPassword);
        }

        private async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            string role = (await _userManager.GetRolesAsync(user))[0];
            byte[] secret = Encoding.ASCII.GetBytes(s: _token.Secret is not null ? _token.Secret : throw new ArgumentNullException(nameof(user)));

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
