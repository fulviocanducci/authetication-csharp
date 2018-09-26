using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppFullFramework.Models;

namespace WebAppFullFramework.Controllers
{
    [RoutePrefix("admin")]
    [Authorize()]
    public class AdminController : Controller
    {
        private ApplicationSignInManager SignInManager 
            => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        private ApplicationUserManager UserManager
            => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        
        #region CodeSupport
        //public AdminController()
        //{
        //    SignInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //}

        //#region Configuration
        //public AdminController(ApplicationSignInManager signInManager, ApplicationUserManager userManager )
        //{
        //    SignInManager = signInManager ?? throw new System.ArgumentNullException(nameof(signInManager));
        //    UserManager = userManager ?? throw new System.ArgumentNullException(nameof(userManager));
        //    SetConfigurationUserManager();
        //}        
        //private void SetConfigurationUserManager()
        //{
        //    var dataProtectionProvider = new DpapiDataProtectionProvider("ASPNETIdentity");
        //    UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASPNETIdentity"));
        //    UserManager.EmailService = new EmailService();
        //}
        //#endregion
        #endregion

        public ActionResult Index()
        {           
            return View();
        }

        #region AllowAnonymous_Methods
        [HttpGet()]
        [AllowAnonymous()]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost()]
        [AllowAnonymous()]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                SignInStatus result = await SignInManager
                    .PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, shouldLockout: false);
                if (result == SignInStatus.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Login e senha inválidos.");
                }                
            }
            return View();
        }        

        [HttpGet()]
        [AllowAnonymous()]
        public ActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost()]
        [AllowAnonymous()]
        public async Task<ActionResult> RecoverPassword(ForgotViewModel forgotViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(forgotViewModel.Email);
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                StringBuilder body = new StringBuilder();
                body.Append("<div style=\"text-align:center\">");
                body.Append("<h3>Recuperação de Senha</h3>");
                body.Append("<div>Clique no botão para recuperar a senha: ");
                body.AppendFormat("<a href=\"{0}\">Recuperar</a>", Url.Action("ResetPassword","Admin", new { code, user.Email }, protocol: Request.Url.Scheme) );
                body.Append("</div>");
                body.Append("</div>");
                await UserManager.SendEmailAsync(user.Id, "Recuperação de Senha", body.ToString());
                ViewBag.Status = true;
            }
            return View();
        }

        [HttpGet()]
        [AllowAnonymous()]
        public ActionResult ResetPassword(string code, string email)
        {
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(email))
            {
                return View(new ResetPasswordViewModel
                {
                    Code = code,
                    Email = email
                });
            }
            return HttpNotFound();
        }

        [HttpPost()]
        [AllowAnonymous()]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            ViewBag.Link = false;
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByEmail(resetPasswordViewModel.Email);
                if (user != null)
                {
                    var result = await UserManager
                        .ResetPasswordAsync(user.Id, resetPasswordViewModel.Code, resetPasswordViewModel.Password);
                    if (result.Succeeded)
                    {
                        ViewBag.Link = true;
                        ViewBag.Status = "Senha alterada com êxito";
                    }
                    else
                    {
                        AddModelStateErrors(result.Errors);                        
                    }
                    return View();
                }
            }
            return View();
        }        
        #endregion

        [HttpGet()]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    ViewBag.Status = true;
                    return View();
                }
                else
                {
                    AddModelStateErrors(result.Errors);
                }
            }
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext
                .GetOwinContext()
                .Authentication
                .SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [NonAction()]
        private void AddModelStateErrors(IEnumerable<string> errors)
        {
            errors.ToList()
                .ForEach(x =>
                {
                    ModelState.AddModelError("", x);
                });
        }
    }
}