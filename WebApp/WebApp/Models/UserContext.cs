using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> option) : base(option)
        {
            
        }
        
            
        public DbSet<User> Users { get; set; }
    }
}
