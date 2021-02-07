using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SwGlobal.App_Start;
using SwGlobal.Models;
using SwGlobal.Notification;
using SwGlobalData.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SwGlobal.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private SwGlobalDbContext unitOfWork = new SwGlobalDbContext();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set
            {
                _userManager = value;
            }
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    TempData["alert_data"] = new Alert("Please logout of this account before attempting to login with another", AlertStatus.Error);
                    return Redirect("/");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                ApplicationUser user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    TempData["InvalidAuth"] = true;
                    ModelState.AddModelError("", "Email does not exist");
                    return View(model);
                }
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        TempData["InvalidAuth"] = true;
                        ModelState.AddModelError("", "Invalid login attempt. Please click the button below to register or reset your password");
                        return View(model);
                }
            }
            catch (HttpAntiForgeryException)
            {
                TempData["alert_data"] = new Alert("Please logout of this account before attempting to login with another", AlertStatus.Error);
                return Redirect("/");
            }
            catch (Exception ex)
            {
                TempData["alert_data"] = new Alert("Please logout of this account before attempting to login with another", AlertStatus.Error);
                return Redirect("/");
            }
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
           
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber,FullName = model.FullName, Address = model.HomeAddress };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        string roleToAdd = "Customer";
                        await UserManager.AddToRoleAsync(user.Id, roleToAdd);
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        //this should be done with hangfire.........
                        //string token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = token }, protocol: Request.Url.Scheme);
                        //BackgroundTask bg = new BackgroundTask();
                        //bg.SendNotifyToCustomerOnNewRegistration(user.Id, callbackUrl);
                        TempData["action_data"] = new Alert("Your registration was successful", AlertStatus.Success);
                        return RedirectToAction("Index", roleToAdd + "/Dashboard");
                    }
                    AddErrors(result);
                }
                // If we got this far, something failed, redisplay form
                
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }

        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
