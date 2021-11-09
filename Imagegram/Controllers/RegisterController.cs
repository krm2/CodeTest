using System;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Imagegram.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            ILogger<RegisterController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                var user = new IdentityUser {UserName = registerModel.Email, Email = registerModel.Email};
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"LogError creating user: {ex.Message}", ex);
            }

            return BadRequest("Something went wrong contact support");
        }
    }
}
