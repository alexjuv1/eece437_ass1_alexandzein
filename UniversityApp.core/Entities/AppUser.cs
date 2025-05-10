using Microsoft.AspNetCore.Identity;

namespace UniversityApp.Core.Entities
{
    public class AppUser : IdentityUser
    {
        // You can add custom properties if needed
        public string? FullName { get; set; }
    }
}
