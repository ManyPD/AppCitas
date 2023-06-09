using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AppCitas.Service.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }

    }f
}
