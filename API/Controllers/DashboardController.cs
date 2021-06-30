using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class DashboardController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;
        private readonly ILinksRepository _linksRepository;
        public DashboardController(DataContext context, IUsersRepository usersRepository, ILinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
            _usersRepository = usersRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ProfileDto>> GetUserInfo()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);

            var user = await _context.Users
                            .SingleOrDefaultAsync(u => u.UserName == username);
            // var user = await _usersRepository.GetUserByUsernameAsync(username);

            var links = _context.Links.Where(l => l.AppUserId == user.Id).ToList();
            // var links = _linksRepository.GetLinksByUserId(user.Id);

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

        [HttpPut]
        public async Task<ActionResult> UpdateUserInfo(ProfileDto profileUpd)
        {
            var user = await _context.Users
                            .SingleOrDefaultAsync(u => u.UserName == User.FindFirstValue(ClaimTypes.Name));

            user.KnownAs = profileUpd.KnownAs;
            user.PhotoUrl = profileUpd.PhotoUrl;
            user.UserBio = profileUpd.UserBio;
            user.Links = profileUpd.Links;

            // Add to database
            //_context.Entry(user).State = EntityState.Modified; // Add a flag to the entity to say that's been modified
            // _context.Attach(user);
            _usersRepository.Update(user);

            // await _context.SaveChangesAsync();
            if (await _usersRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user info");
        }


        [HttpPost]
        public async Task<ActionResult<Links>> AddUserLinks(LinksDto addLink)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);

            var user = await _context.Users
                            .SingleOrDefaultAsync(u => u.UserName == username);

                var newLink = new Links
                {
                    Id = addLink.Id,
                    LinkName = addLink.LinkName,
                    LinkUrl = addLink.LinkUrl,
                    AppUserId = user.Id
                };
                // _context.Links.Add(newLink);
                _linksRepository.AddLink(newLink);
            

            // await _context.SaveChangesAsync();
            if (await _usersRepository.SaveAllAsync()) return Ok();

            return BadRequest("Fail to add the new links");
        }

        [HttpDelete("link/{linkId}")]
        public async Task<ActionResult> DeleteLink(int linkId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var link = await _context.Links.FindAsync(linkId);

            if (int.Parse(userId) != link.AppUserId) return Unauthorized();

            // _context.Links.Remove(link);
            _linksRepository.DeleteLink(link);

            // if (await _context.SaveChangesAsync() > 0) return Ok();
            if (await _usersRepository.SaveAllAsync()) return Ok();

            return BadRequest("Cannot delete the link!");

        }

        
        // Delete account:
        [HttpDelete("delete-account")]
        public async Task<ActionResult> DeleteAccount()
        {
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                var user = await _context.Users
                            .SingleOrDefaultAsync(u => u.Id == userId);

                _usersRepository.Delete(user);
                
                if (await _usersRepository.SaveAllAsync()) return Ok();

                return BadRequest("Cannot delete the user!");
            }

            return Unauthorized();
        }

    }
}