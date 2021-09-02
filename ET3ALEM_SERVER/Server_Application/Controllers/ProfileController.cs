using System.Threading.Tasks;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Server_Application.Controllers
{
    [Authorize]
    [Route("api/Profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private const string PasswordRecoveryToken = "PasswordRecoveryToken";
        private const string RefreshToken = "RefreshToken";
        private readonly IConfiguration _iConfiguration;
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _iConfiguration = configuration;
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVm)
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var isValidPassword = await _userManager.CheckPasswordAsync(user, changePasswordVm.OldPassword);
            if (!isValidPassword) return BadRequest("Old password does is incorrect");
            var result =
                await _userManager.ChangePasswordAsync(user, changePasswordVm.OldPassword,
                    changePasswordVm.NewPassword);
            return result.Succeeded ? Ok() : BadRequest("Unable to update the password");
        }

        [HttpGet("GetUserEmail")]
        public async Task<IActionResult> GetUserEmail()
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("Invalid User");

            var userEmail = (await _userManager.FindByIdAsync(userId)).Email;
            return Ok(new {email = userEmail});
        }
    }
}