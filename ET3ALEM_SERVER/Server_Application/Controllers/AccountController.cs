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
using Microsoft.AspNetCore.Authorization;
using BusinessEntities.Models;
using DataServiceLayer;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Globalization;

namespace Server_Application.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _IConfiguration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration IConfiguration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _IConfiguration = IConfiguration;

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

            string savedRefreshToken = await GetRefreshToken(user);
            if (savedRefreshToken != tokens.RefreshToken)
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
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("Logout")]
        public async Task Logout()
        {
            var userPrincipal = HttpContext.User;
            if (userPrincipal != null)
            {
                await deleteRefreshToken(await _userManager.GetUserAsync(userPrincipal));
            }
            await _signInManager.SignOutAsync();

        }

        private async Task saveRefreshToken(User user, string newRefreshToken)
        {
            await _userManager.SetAuthenticationTokenAsync(user, "UserRefresh", "RefreshToken", newRefreshToken);
        }

        private async Task deleteRefreshToken(User user)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, "UserRefresh", "RefreshToken");
        }

        private async Task<string> GetRefreshToken(User user)
        {
            return await _userManager.GetAuthenticationTokenAsync(user, "UserRefresh", "RefreshToken");
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Authentication:JwtKey"])),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
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

        [HttpPost("sendRecoveryMail")]
        public async Task<IActionResult> SendRecoveryMail(ResetPasswordVM resetPasswordVM)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
            if (user == null)
                return NotFound("Invalid Email");
            var apiKey = _IConfiguration.GetSection("EmailConfiguration").GetValue<string>("ApiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_IConfiguration.GetSection("EmailConfiguration").GetValue<string>("Sender"), _IConfiguration.GetSection("EmailConfiguration").GetValue<string>("User"));
            var to = new EmailAddress(user.Email, user.UserName);
            var subject = "Reset Password";
            // remove previous token
            await _userManager.RemoveAuthenticationTokenAsync(user, "UserPasswordRecovery", "PasswordRecoveryToken");
            var passwordRecoveryToken = GenerateRecoveryToken(user.Email);
            await _userManager.SetAuthenticationTokenAsync(user, "UserPasswordRecovery", "PasswordRecoveryToken", passwordRecoveryToken);
            var resetUrl = $"{ _IConfiguration.GetValue<string>("ClientUrl")}/auth/reset?token={passwordRecoveryToken}";
            var htmlContent = $@"<a href=""{resetUrl}"">follow this link to reset your password</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            _ = client.SendEmailAsync(msg).ConfigureAwait(false);
            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            string email = new JwtSecurityToken(resetPasswordVM.RecoveryToken).Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
            User user = await _userManager.FindByEmailAsync(email);
            string recoveryToken = await _userManager.GetAuthenticationTokenAsync(user, "UserPasswordRecovery", "PasswordRecoveryToken");
            var token = recoveryToken != null ? new JwtSecurityToken(recoveryToken) : null;
            if (user == null || token?.ValidTo < DateTime.UtcNow || string.IsNullOrEmpty(recoveryToken))
            {
                if (user != null)
                    await _userManager.RemoveAuthenticationTokenAsync(user, "UserPasswordRecovery", "PasswordRecoveryToken");
                return NotFound();
            }
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordVM.Password);
            if (passwordChangeResult.Succeeded)
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "UserPasswordRecovery", "PasswordRecoveryToken");
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private string GenerateRecoveryToken(string email)
        {
            return GenerateJwt(new List<Claim> { new Claim(JwtRegisteredClaimNames.Sub, email) }, 15);
        }


    }
}
