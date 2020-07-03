using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server_Application.Models.Account;
using System.Security.Cryptography;
using BusinessEntities.ViewModels;

namespace Server_Application.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(registerVM);
            }
            var user = new IdentityUser { Email = registerVM.Email, UserName = registerVM.Email };
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                //foreach (var error in result.Errors)
                //{
                ModelState.TryAddModelError(result.Errors.First().Code, result.Errors.First().Description);
                // }

                return BadRequest(ModelState);
            }

            await _signInManager.SignInAsync(user, false);

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

            Tokens newTokens = new Tokens(GenerateJwt(claims), GenerateRefreshToken());
            await saveRefreshToken(user, newTokens.RefreshToken);
            return Ok(newTokens);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null &&
                await _userManager.CheckPasswordAsync(user, loginVM.Password))
            {
                await _signInManager.SignInAsync(user, false);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

                Tokens newTokens = new Tokens(GenerateJwt(claims), GenerateRefreshToken());
                await deleteRefreshToken(user);
                await saveRefreshToken(user, newTokens.RefreshToken);
                return Ok(newTokens);
            }

            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Refresh(string token, string refreshToken)
        {
            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(token);
            string username = principal.Identity.Name;
            IdentityUser user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return BadRequest();
            }

            string savedRefreshToken = await GetRefreshToken(user);
            if (savedRefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            try
            {
                string newJWT = GenerateJwt(principal.Claims);
                string newRefreshToken = GenerateRefreshToken();
                await deleteRefreshToken(user);
                await saveRefreshToken(user, newRefreshToken);
                return Ok(new Tokens(newJWT, newRefreshToken));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

        }

        private async Task saveRefreshToken(IdentityUser user, string newRefreshToken)
        {
            await _userManager.SetAuthenticationTokenAsync(user, "UserRefresh", "RefreshToken", newRefreshToken);
        }

        private async Task deleteRefreshToken(IdentityUser user)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, "UserRefresh", "RefreshToken");
        }

        private async Task<string> GetRefreshToken(IdentityUser user)
        {
            return await _userManager.GetAuthenticationTokenAsync(user, "UserRefresh", "RefreshToken");
        }

        private string GenerateJwt(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Authentication:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Authentication:JwtIssuer"],
                _configuration["Authentication:JwtIssuer"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the server key used to sign the JWT token is here, use more than 16 chars")),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            //check that the algorithm used to sign key is valid
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
                throw new SecurityTokenException("Invalid Token");

            return principal;

        }
    }
}
