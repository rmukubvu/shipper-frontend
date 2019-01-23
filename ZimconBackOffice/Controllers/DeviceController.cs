using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;

namespace ZimconBackOffice.Controllers
{
    public class DeviceController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();
        // GET: Device
        public ActionResult Index(string id = null)
        {
            var devices = _rest.GetSmartDevices();
            var vehicles = _rest.GetVehicles();
            var model = new DeviceAllocationViewModel()
            {
                Devices = devices,
                Vehicles = vehicles
            };
            return View(model);
        }
    }

    public class DeviceAllocationViewModel
    {
        public List<SmartDevice> Devices { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}