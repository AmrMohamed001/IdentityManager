using System.ComponentModel.DataAnnotations;

namespace Role_Identity.Models.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
