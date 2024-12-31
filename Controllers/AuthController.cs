using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Role_Identity.Models;
using Role_Identity.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Role_Identity.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        public IActionResult Error()
        {
            return View("Error");
        }
        private void Error(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }
        #region Register
        [HttpGet]
        public IActionResult Register(string returnurl = null)
        {
            ViewData["returnurl"] = returnurl;
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnurl = null)
        {
            ViewData["returnurl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = new MailAddress(model.Email).User,
                    Name = model.Name,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // send mail
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("RegisterConfirmation", "Auth", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);
                    await emailSender.SendEmailAsync(user.Email, "Register-Confirmation", $"Visit this url to confirm email : <a href={callbackUrl}>Link</a>");

                    // redirect to registeremailsent
                    return RedirectToAction("RegisterEmailSent");
                }
                Error(result);
            }
            return View("Register", model);
        }
        public IActionResult RegisterEmailSent()
        {
            return View("RegisterEmailSent");
        }

        public async Task<IActionResult> RegisterConfirmation(string code, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return View("Error");
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            return View("Error");
        }

        #endregion


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult SignIn(string returnurl = null)
        {
            ViewData["returnurl"] = returnurl;
            return View("SignIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel model, string returnurl = null)
        {
            ViewData["returnurl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            var username = new EmailAddressAttribute().IsValid(model.Email) ? userManager.FindByEmailAsync(model.Email).Result.UserName : model.Email;
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(username, model.Password, model.RememberMe, true);
                if (result.Succeeded) return LocalRedirect(returnurl);
                if (result.IsLockedOut) return View("Lockout");
                else
                {
                    ModelState.AddModelError("", "Email or password is Invalid ");
                    return View("SignIn", model);
                }
            }
            return View("SignIn", model);
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View("ForgetPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null) return View("ForgetPassword", model);
                var code = userManager.GeneratePasswordResetTokenAsync(user).Result;
                var callbackUrl = Url.Action("ResetPassword", "Auth", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);
                await emailSender.SendEmailAsync(user.Email, "Reset-Password", $"Visit this url to reset password : <a href={callbackUrl}>Link</a>");
                return RedirectToAction("ForgetPasswordConfirmation");
            }
            return View("ForgetPassword", model);
        }

        [HttpGet]
        public IActionResult ForgetPasswordConfirmation()
        {
            return View("ForgetPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            return (code == null) ? View("Error") : View("ResetPassword");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null) return RedirectToAction("ResetPasswordConfirmation");
                var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded) return RedirectToAction("ResetPasswordConfirmation");
                Error(result);
            }
            return View("ResetPassword", model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View("ResetPasswordConfirmation");
        }
    }
}
