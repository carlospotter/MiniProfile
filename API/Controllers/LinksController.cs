using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LinksController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILinksRepository _linksRepository;
        private readonly IUsersRepository _usersRepository;
        public LinksController(DataContext context, ILinksRepository linksRepository, IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _linksRepository = linksRepository;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Links>> GetLinks(int id)
        {
            // var links = await _context.Links.FindAsync(id);
            var link = await _linksRepository.GetLinksByLinkId(id);

            return link;
        }

        // [HttpPost("{userId}")]
        // public async Task<ActionResult<Links>> AddLinks(Links links, int userId)
        // {
        //     var link = new Links
        //     {
        //         Id = links.Id,
        //         LinkName = links.LinkName,
        //         LinkUrl = links.LinkUrl,
        //         AppUserId = userId
        //     };

        //     // _context.Links.Add(link);
        //     _linksRepository.AddLink(link);
        //     // await _context.SaveChangesAsync();
        //     if (await _usersRepository.SaveAllAsync()) return Ok(link);

        //     return BadRequest("Cannot add link!");
        // }
    }
}