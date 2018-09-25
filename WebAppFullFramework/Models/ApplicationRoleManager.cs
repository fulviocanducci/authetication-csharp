using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace WebAppFullFramework.Models
{
    public class ApplicationRoleManager : RoleManager<IdentityRole, string>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
    }
}