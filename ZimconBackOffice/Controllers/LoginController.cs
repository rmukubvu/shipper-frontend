
using System.Web.Mvc;

namespace ZimconBackOffice.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LockScreen()
        {
            return View();
        }
    }
}