using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;

namespace ZimconBackOffice.Controllers
{
    public class UserController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();
        // GET: Device
        public ActionResult Index(string id = null)
        {
            var user = _rest.GetUser();

            return View(user);
        }
    }
}