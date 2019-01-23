using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipper.RestGateway.RestClients;

namespace ZimconBackOffice.Controllers
{
    public class VehicleController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();
        // GET: Vehicle
        public ActionResult Index()
        {
            var vehicles = _rest.GetVehicles();
            return View(vehicles);
        }
    }
}