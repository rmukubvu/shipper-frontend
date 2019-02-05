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
            var locations = _rest.GetVehicleCurrentLocations();

            var dashboard = new DashboardViewModel()
            {
                vehicleCount = vehicles.Count,
                smartDevicesCount = devices.Count,
                devices = devices,
                vehicles = vehicles,
                deviceAllocations = allocations,
                VehicleLocations = Transpose(locations)
            };
            return View(dashboard);
        }

        private List<TruckCoordinates> Transpose(List<VehicleLocation> collection)
        {
            var result = new List<TruckCoordinates>();
            foreach (var item in collection)
            {
                result.Add(new TruckCoordinates()
                {
                    lat = item.latitude,
                    lng = item.longitude
                });
            }
            return result;
        }
    }
}