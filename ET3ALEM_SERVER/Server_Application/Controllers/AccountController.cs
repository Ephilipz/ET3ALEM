using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;
using DataServiceLayer;
using ExceptionHandling.CustomExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server_Application.Models.Account;
using TokenHandler = Server_Application.Models.Account.TokenHandler;

namespace Server_Application.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private const string PasswordRecoveryToken = "PasswordRecoveryToken";
        private const string RefreshToken = "RefreshToken";
        private readonly IEmailDsl _iEmailDsl;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenHandler _tokenHandler;
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IConfiguration configuration, IEmailDsl emailDsl)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = new TokenHandler(configuration);
            _iEmailDsl = emailDsl;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return BadRequest(registerVM);
            var user = new User {FullName = registerVM.Name, Email = registerVM.Email, UserName = registerVM.Email};
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                throw new CustomExceptionBase(result.Errors.First().Description);
            }

            await _signInManager.SignInAsync(user, false);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            var newTokens = new Tokens(_tokenHandler.GenerateJwt(claims), _tokenHandler.GenerateRefreshToken(),
                user.Id);
            await SaveToken(user, newTokens.RefreshToken, RefreshToken);
            return Ok(newTokens);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            if (!ModelState.IsValid) return BadRequest(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null &&
                await _userManager.CheckPasswordAsync(user, loginVM.Password))
            {
                await _signInManager.SignInAsync(user, false);

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, user.Email),
                    new(ClaimTypes.NameIdentifier, user.Id)
                };

                var newTokens = new Tokens(_tokenHandler.GenerateJwt(claims), _tokenHandler.GenerateRefreshToken(),
                    user.Id);
                await DeleteToken(user, RefreshToken);
                await SaveToken(user, newTokens.RefreshToken, RefreshToken);
                return Ok(newTokens);
            }

            throw new CustomExceptionBase("Invalid UserName or Password");
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] Tokens tokens)
        {
            var principal = _tokenHandler.GetPrincipalFromExpiredToken(tokens.JWT);
            var username = principal?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest();

            var savedRefreshToken = await GetToken(user, RefreshToken);
            if (savedRefreshToken != tokens.RefreshToken) throw new CustomExceptionBase("Invalid refresh token");

            try
            {
                var newJWT = _tokenHandler.GenerateJwt(principal.Claims);
                var newRefreshToken = _tokenHandler.GenerateRefreshToken();
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
                if (userPrincipal.Claims.Any())
                {
                    await DeleteToken(await _userManager.GetUserAsync(userPrincipal), RefreshToken);
                }
            }
            catch (Exception)
            {
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
                throw new CustomExceptionBase("No user found with this email");
            await DeleteToken(user, PasswordRecoveryToken);
            var claims = new List<Claim> {new(JwtRegisteredClaimNames.Sub, user.Email)};
            var recoveryToken = _tokenHandler.GenerateJwt(claims, 15);

            await SaveToken(user, recoveryToken, PasswordRecoveryToken);
            await SendPasswordRecoveryMail(recoveryToken, user);
            return Ok();
        }

        private async Task SendPasswordRecoveryMail(string recoveryToken, User user)
        {
            var htmlContent = GenerateRecoveryEmailHTMLContent(recoveryToken);
            await _iEmailDsl.SendEmail("Reset Password", string.Empty, htmlContent, user.Email, user.UserName);
        }

        private string GenerateRecoveryEmailHTMLContent(string recoveryToken)
        {
            var resetUrl =
                $"{_tokenHandler._IConfiguration.GetValue<string>("ClientUrl")}/auth/reset?token={recoveryToken}";
            var htmlContent = $@"<a href=""{resetUrl}"">follow this link to reset your password</a>";
            return htmlContent;
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var user = await GetUserForRecoverToken(resetPasswordVM);
            if (user != null)
            {
                var recoveryToken = await GetToken(user, PasswordRecoveryToken);
                JwtSecurityToken token = null;
                if (recoveryToken != null)
                    token = new JwtSecurityToken(recoveryToken);
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

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordChangeResult =
                await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordVM.Password);
            if (passwordChangeResult.Succeeded)
            {
                await DeleteToken(user, PasswordRecoveryToken);
                return Ok();
            }

            return NotFound();
        }

        private async Task<User> GetUserForRecoverToken(ResetPasswordVM resetPasswordVM)
        {
            var email = new JwtSecurityToken(resetPasswordVM.RecoveryToken)
                .Claims
                .FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?
                .Value;
            var user = await _userManager.FindByEmailAsync(email);
            return user;
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
                _ => throw new CustomExceptionBase("Invalid token name")
            };
        }
    }
}