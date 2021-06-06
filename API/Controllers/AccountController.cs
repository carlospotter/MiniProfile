using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> AddUser(RegisterDto newUser)
        {
            if (await UserExists(newUser.Username)) return BadRequest("Username is taken!");

            var user = new AppUser
            {
                UserName = newUser.Username,
                KnownAs = newUser.KnownAs,
                PhotoUrl = newUser.PhotoUrl,
                UserBio = newUser.UserBio
            };

            // _context.Users.Add(user);
            // await _context.SaveChangesAsync();
            var result = await _userManager.CreateAsync(user, newUser.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.GenerateToken(user),
                KnownAs = user.KnownAs,
                PhotoUrl = user.PhotoUrl
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto userLogin)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == userLogin.Username);

            if (user == null) return Unauthorized("Invalid login information!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false); // false: do not lock out user if login info is wrong

            if (!result.Succeeded) return Unauthorized("Invalid login information!");

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.GenerateToken(user),
                KnownAs = user.KnownAs,
                PhotoUrl = user.PhotoUrl
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }




    }
}