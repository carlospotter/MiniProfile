using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;
        private readonly ILinksRepository _linksRepository;
        public UsersController(DataContext context, IUsersRepository usersRepository, ILinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
            _usersRepository = usersRepository;
            _context = context;

        }

        [HttpGet("{username}")]
        public async Task<ActionResult<ProfileDto>> GetUserByUsername(string username)
        {
            // var user = await _context.Users
            //                     // .Include(l => l.Links)
            //                     .SingleOrDefaultAsync(u => u.UserName == username);
            var user = await _usersRepository.GetUserByUsernameAsync(username);

            // var links = _context.Links.Where(l => l.AppUserId == user.Id).ToList();
            var links = _linksRepository.GetLinksByUserId(user.Id);

            return new ProfileDto
            {
                Id = user.Id,
                Username = user.UserName,
                KnownAs = user.KnownAs,
                PhotoUrl = user.PhotoUrl,
                UserBio = user.UserBio,
                Links = links
            };
        }
    }
}