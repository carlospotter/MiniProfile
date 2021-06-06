using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.Repositories
{
    public class LinksRepository : ILinksRepository
    {
        private readonly DataContext _context;
        public LinksRepository(DataContext context)
        {
            _context = context;
        }

        public void AddLink(Links newLink)
        {
            _context.Links.Add(newLink);
        }

        public void DeleteLink(Links delLink)
        {
            _context.Links.Remove(delLink);
        }

        public ICollection<Links> GetLinksByUserId(int userId)
        {
            return _context.Links.Where(l => l.AppUserId == userId).ToList();
        }

        public async Task<Links> GetLinksByLinkId(int linkId)
        {
            return await _context.Links.FindAsync(linkId);
        }
    }
}