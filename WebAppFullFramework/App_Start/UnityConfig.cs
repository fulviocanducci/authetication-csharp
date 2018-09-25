using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Web;
using Unity;
using Unity.Injection;
using WebAppFullFramework.Models;
namespace WebAppFullFramework
{    
    public static class UnityConfig
    {        
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });
        public static IUnityContainer Container => container.Value;        
        
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<RoleManager<IdentityRole>>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<RoleManager<IdentityRole, string>, ApplicationRoleManager>();
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<ApplicationRoleManager>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>();
        }
    }
}