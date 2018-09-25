using System.Web.Mvc;

namespace WebAppFullFramework.Controllers
{
    [RoutePrefix("admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}