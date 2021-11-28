using System.Threading.Tasks;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;
using ExceptionHandling.CustomExceptions;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Server_Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAccountHelper _accountHelper;

        public ProfileController(UserManager<User> userManager, IAccountHelper accountHelper)
        {
            _userManager = userManager;
            _accountHelper = accountHelper;
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVm)
        {
            var userId = _accountHelper.GetUserId(HttpContext, User);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var isValidPassword = await _userManager.CheckPasswordAsync(user, changePasswordVm.OldPassword);
            if (!isValidPassword)
            {
                throw new CustomExceptionBase("Old password does is incorrect");
            }

            var result = await _userManager
                    .ChangePasswordAsync(user, changePasswordVm.OldPassword, changePasswordVm.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }

            throw new CustomExceptionBase("Unable to reset password");
        }

        [HttpGet("GetUserEmail")]
        public async Task<IActionResult> GetUserEmail()
        {
            var userId = _accountHelper.GetUserId(HttpContext, User);
            if (string.IsNullOrWhiteSpace(userId)) throw new CustomExceptionBase("Invalid User");

            var userEmail = (await _userManager.FindByIdAsync(userId)).Email;
            return Ok(new {email = userEmail});
        }
    }
}