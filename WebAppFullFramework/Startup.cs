using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppFullFramework.Startup))]
namespace WebAppFullFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
