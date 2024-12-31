using System.ComponentModel.DataAnnotations;

namespace Role_Identity.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password dosn't match Confirm ")]
        public string ConfirmPassword { get; set; }
    }
}
