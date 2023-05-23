using EcommerceAPI.Application.DTOs.Identity;
using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Identity.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EcommerceAPI.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<CreateUserResponseDTO> CreateUserAsync(CreateUserRequestDTO user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded)
                await _userManager.SetLockoutEnabledAsync(identityUser, false);

            var response = new CreateUserResponseDTO(result.Succeeded);
            if (!result.Succeeded && result.Errors.Any())
                response.AddErrors(result.Errors.Select(s => s.Description).ToList());

            return response;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, true);
            if (result.Succeeded)
                return await GenerateJwtTokenAsync(user.Email);

            var userLoginResponse = new LoginResponseDTO(result.Succeeded);

            if (result.IsLockedOut)
                userLoginResponse.AddError("This user is blocked.");
            else if (result.IsNotAllowed)
                userLoginResponse.AddError("This user is not allowed to login.");
            else if (result.RequiresTwoFactor)
                userLoginResponse.AddError("You need to confirm the login in your second factor authentication method.");
            else
                userLoginResponse.AddError("Incorrect username or password.");

            return userLoginResponse;
        }

        private async Task<LoginResponseDTO> GenerateJwtTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var tokenClaims = await GetUserClaimsAsync(user);

            var expirationDate = DateTime.Now.AddSeconds(_jwtOptions.JwtOptionsExpiration);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.JwtOptionsIssuer,
                audience: _jwtOptions.JwtOptionsAudience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: expirationDate,
                signingCredentials: _jwtOptions.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new LoginResponseDTO
            (
                success: true,
                token: token,
                expirationDate: expirationDate
            );
        }

        private async Task<IList<Claim>> GetUserClaimsAsync(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }
    }
}
