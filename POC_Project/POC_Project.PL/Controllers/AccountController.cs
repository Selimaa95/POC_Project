using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POC_Project.BL.VModels;
using POC_Project.DAL.Extend;
using System.Diagnostics;

namespace POC_Project.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Prop
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        #endregion

        #region Ctor
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #endregion

        #region Actions

        #region Registration
        
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AccountVM model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                IsAgree = model.IsAgree
            };

            var result = await userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Login");

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);

        }


        #endregion

        #region Login
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountVM model)
        {
            var userName = await userManager.FindByNameAsync(model.UserName);
            var userEmail = await userManager.FindByEmailAsync(model.UserName);

            dynamic result;
            if(userEmail != null)
            {
                result = await signInManager.PasswordSignInAsync(userEmail, model.Password, model.RememberMe,false);
            }
            else
            {
                result = await signInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe, false);
            }

            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return View(model);
        }

        #endregion

        #region SignOut
        
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion

        #region ForgetPassword
        
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(AccountVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if(user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action("ResetPassword","Account", new { Email = model.Email, Token = token}, Request.Scheme);
                EventLog log = new EventLog();
                log.Source = "Inventory System";
                log.WriteEntry(link,EventLogEntryType.Information);

                return RedirectToAction("ConfirmForgetPassword");

            }

            return RedirectToAction("ConfirmForgetPassword");
        }

        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        #endregion

        #region ResetPassword
        
        [HttpGet]
        public IActionResult ResetPassword(string? Email, string? Token)
        {
            if(Email != null && Token != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ForgetPassword");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(AccountVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ConfirmResetPassword");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);

            }
                return RedirectToAction("ConfirmResetPassword");

        }
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
        
        #endregion

        #endregion

    }
}
