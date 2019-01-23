using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;

namespace ZimconBackOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();

        public ActionResult Index()
        {
            var devices = _rest.GetSmartDevices();
            var vehicles = _rest.GetVehicles();
            var allocations = _rest.GetSmartDeviceVehicleMapping();
            var dashboard = new DashboardViewModel()
            {
                vehicleCount = vehicles.Count,
                smartDevicesCount = devices.Count,
                devices = devices,
                vehicles = vehicles,
                deviceAllocations = allocations
            };
            return View(dashboard);
        }
        
    }

    public class DashboardViewModel
    {
        public int vehicleCount { get; set; }
        public int smartDevicesCount { get; set; }
        public List<SmartDevice> devices { get; set; }
        public List<Vehicle> vehicles { get; set; }
        public List<SmartDeviceAllocation> deviceAllocations { get; set; }
    }
}