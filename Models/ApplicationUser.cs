using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Role_Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
