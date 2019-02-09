using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;
using ZimconBackOffice.Models;

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

        public ActionResult VehiclesLocation()
        {
            var locations = _rest.GetVehicleCurrentLocations();
            List<TruckCoordinates> coordinates = Transpose(locations);
            return View(coordinates);
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