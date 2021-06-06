using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface ILinksRepository
    {
        ICollection<Links> GetLinksByUserId(int userId);
        void AddLink(Links newLink);
        void DeleteLink(Links delLink);
        Task<Links> GetLinksByLinkId(int linkId);
    }
}