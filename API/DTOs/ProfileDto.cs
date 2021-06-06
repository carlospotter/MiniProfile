using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class ProfileDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string KnownAs { get; set; }

        public string PhotoUrl { get; set; }

        public string UserBio { get; set; }

        public ICollection<Links> Links { get; set; }
    }
}