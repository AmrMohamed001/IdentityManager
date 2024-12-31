using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Role_Identity.Models;

namespace Role_Identity.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
