using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZimconBackOffice.Controllers
{
    public class DriverController : Controller
    {
        private RestCalls rest = new RestCalls();
        // GET: Driver
        public ActionResult Index()
        {
            var driver = rest.GetDrivers();
            var vehicles = rest.GetVehicles();
            var allocation = rest.GetVehicleDriverViewModel();
            var countries = rest.GetCountries();
            var model = new DriverViewModel()
            {
                Drivers = driver,
                Vehicles = vehicles,
                DriverVehicleAllocation = allocation,
                Countries = countries
            };
            return View(model);
        }

        public class DriverViewModel
        {
            public List<Driver> Drivers { get; set; }
            public List<Vehicle> Vehicles { get; set; }
            public List<Country> Countries { get; set; }
            public List<VehicleDriverAllocation> DriverVehicleAllocation { get; set; }
        }
    }
}