using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SbBlog.API.Models.Entities.Account;
using SbBlog.API.Models.Entities;
using SbBlog.API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SbBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JWTService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AccountController(JWTService jwtService,
           SignInManager<User> signInManager,
           UserManager<User> userManager)
        {
            _jwtService = jwtService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email)?.Value);
            return CreateApplicationUserDto(user);

        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return Unauthorized("Invalid Username and Password");

            if (user.EmailConfirmed == false) return Unauthorized("Please Confirm your Email.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid Username and Password");

            return CreateApplicationUserDto(user);
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {

            if (await CheackEmailExistsAsync(model.Email))
            {
                return BadRequest($"An existing account is using {model.Email},email address. Please try with another email address");
            }
            var userToAdd = new User
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                UserName = model.Email.ToLower(),
                Email = model.Email.ToLower(),
                EmailConfirmed = true

            };

            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Your Account has been created, You can login");
        }


        #region Private Helper Methods

        private UserDto CreateApplicationUserDto(User user)
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = _jwtService.CreateJWT(user),
            };
        }



        private async Task<bool> CheackEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }
        #endregion // Close the region

    }
}
