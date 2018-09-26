using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
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
        public ActionResult RecoverPassword(ForgotViewModel forgotViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet()]
        [AllowAnonymous()]
        public ActionResult ResetPassword(string code)
        {
            return View(new ResetPasswordViewModel
            {
                Code = code
            });
        }

        [HttpPost()]
        [AllowAnonymous()]
        public ActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion

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
    }
}