using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Expenses.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using expensesBE.Services;
using expensesBE.Data.DTO.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;


namespace expensesBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        private readonly UserServices _userServices;

        private readonly UserManager<IdentityUser> _userManager;


        public UserController(AppDbContext context, UserServices userServices, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userServices = userServices;
            _userManager = userManager;
        }
       
        [HttpGet("username")]
        public IActionResult GetUsername()
        {
            var username = _userServices.GetUsername(User);

            if (username == null)
            {
                return Unauthorized("User is not authenticated");
            }

            return Ok(new { Username = username });
        }

        [HttpGet("userid")]
        public async Task<IActionResult> GetUserId()
        {
            var userId = await _userServices.GetUserIdAsync(User);

            if (userId == null)
            {
                return Unauthorized("User does not exist.");
            }

            return Ok(new { UserId = userId });
        }

        [HttpPost("generateConfirmationLink")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateConfirmationLink([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                nameof(ConfirmEmail),
                "User",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);

            return Ok(new { ConfirmationLink = callbackUrl });
        }

        [HttpGet("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("Error: User ID or code is null.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Error: User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Email confirmed successfully." });
            }

            return BadRequest("Error confirming your email.");
        }

        [HttpGet("isEmailConfirmed")]
        public async Task<IActionResult> IsEmailConfirmed()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User is not authenticated");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            return Ok(new { EmailConfirmed = user.EmailConfirmed });
        }

        [HttpPost("isEmailInUse")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            // Restituisce un oggetto JSON con la proprietà booleana
            return Ok(new { isEmailInUse = user != null });
        }

    }


}
