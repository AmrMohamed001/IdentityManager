namespace Role_Identity.Models.ViewModels
{
    public class VerifyAuthenticatorViewModel
    {
        public string? Code { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
