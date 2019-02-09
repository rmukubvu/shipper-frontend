using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZimconBackOffice.Models;

namespace ZimconBackOffice.Controllers
{
    public class DashboardController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();
        // GET: Dashboard
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
}