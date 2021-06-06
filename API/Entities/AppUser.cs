using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string KnownAs { get; set; }

        public string PhotoUrl { get; set; }

        public string UserBio { get; set; }

        public ICollection<Links> Links { get; set; }



        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}