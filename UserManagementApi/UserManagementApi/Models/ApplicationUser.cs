// Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace UserManagementApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
