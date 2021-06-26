using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server_Application.Models.Account;
using System.Security.Cryptography;
using BusinessEntities.ViewModels;
using BusinessEntities.Models;
using DataServiceLayer;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Server_Application.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _IConfiguration;
        private readonly IEmailDsl _IEmailDsl;
        private const string PasswordRecoveryToken = "PasswordRecoveryToken";
        private const string RefreshToken = "RefreshToken";

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration IConfiguration, IEmailDsl IEmailDsl)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _IConfiguration = IConfiguration;
            _IEmailDsl = IEmailDsl;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(registerVM);
            }
            var user = new User { FullName = registerVM.Name, Email = registerVM.Email, UserName = registerVM.Email };
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError(result.Errors.First().Code, result.Errors.First().Description);
                return BadRequest(ModelState);
            }

            await _signInManager.SignInAsync(user, false);

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

            Tokens newTokens = new Tokens(GenerateJwt(claims), GenerateRefreshToken(), user.Id);
            await SaveToken(user, newTokens.RefreshToken, RefreshToken);
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

                Tokens newTokens = new Tokens(GenerateJwt(claims), GenerateRefreshToken(), user.Id);
                await DeleteToken(user, RefreshToken);
                await SaveToken(user, newTokens.RefreshToken, RefreshToken);
                return Ok(newTokens);
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] Tokens tokens)
        {
            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(tokens.JWT);
            string username = principal.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            User user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest();
            }

            string savedRefreshToken = await GetToken(user, RefreshToken);
            if (savedRefreshToken != tokens.RefreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            try
            {
                string newJWT = GenerateJwt(principal.Claims);
                string newRefreshToken = GenerateRefreshToken();
                await DeleteToken(user, RefreshToken);
                await SaveToken(user, newRefreshToken, RefreshToken);
                return Ok(new Tokens(newJWT, newRefreshToken, user.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("Logout")]
        public async Task Logout()
        {
            try
            {
                var userPrincipal = HttpContext.User;
                if (userPrincipal != null)
                {
                    await DeleteToken(await _userManager.GetUserAsync(userPrincipal), RefreshToken);
                }
            }
            finally
            {
                await _signInManager.SignOutAsync();
            }
        }
        [HttpPost("sendRecoveryMail")]
        public async Task<IActionResult> SendRecoveryMail(ResetPasswordVM resetPasswordVM)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
            if (user == null)
                return NotFound("Invalid Email");
            // remove previous token
            await DeleteToken(user, PasswordRecoveryToken);
            var recoveryToken = GenerateJwt(new List<Claim> { new Claim(JwtRegisteredClaimNames.Sub, user.Email) }, 15);
            await SaveToken(user, recoveryToken, PasswordRecoveryToken);
            var resetUrl = $"{ _IConfiguration.GetValue<string>("ClientUrl")}/auth/reset?token={recoveryToken}";
            var htmlContent = $@"<a href=""{resetUrl}"">follow this link to reset your password</a>";
            _IEmailDsl.SendEmail("Reset Password", string.Empty, htmlContent, user.Email, user.UserName);
            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            string email = new JwtSecurityToken(resetPasswordVM.RecoveryToken).Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
            User user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string recoveryToken = await GetToken(user, PasswordRecoveryToken);
                var token = recoveryToken != null ? new JwtSecurityToken(recoveryToken) : null;
                if (token?.ValidTo < DateTime.UtcNow || string.IsNullOrEmpty(recoveryToken))
                {
                    await DeleteToken(user, PasswordRecoveryToken);
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordVM.Password);
            if (passwordChangeResult.Succeeded)
            {
                await DeleteToken(user, PasswordRecoveryToken);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private async Task<string> GetToken(User user, string tokenName)
        {
            return await _userManager.GetAuthenticationTokenAsync(user, GetLoginProvider(tokenName), tokenName);
        }

        private async Task DeleteToken(User user, string tokenName)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, GetLoginProvider(tokenName), tokenName);
        }

        private async Task SaveToken(User user, string token, string tokenName)
        {
            await _userManager.SetAuthenticationTokenAsync(user, GetLoginProvider(tokenName), tokenName, token);
        }

        private string GetLoginProvider(string tokenName)
        {
            return tokenName switch
            {
                PasswordRecoveryToken => "UserPasswordRecovery",
                RefreshToken => "UserRefresh",
                _ => throw new ArgumentException("invalid token name"),
            };
        }

        private string GenerateJwt(IEnumerable<Claim> claims, int? duration = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Authentication:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Authentication:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _IConfiguration["Authentication:JwtIssuer"],
                _IConfiguration["Authentication:JwtIssuer"],
                claims,
                expires: DateTime.Now.AddMinutes(duration ?? 5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Authentication:JwtKey"])),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
                //check that the algorithm used to sign key is valid
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
                    throw new SecurityTokenException("Invalid Token");
                return principal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
