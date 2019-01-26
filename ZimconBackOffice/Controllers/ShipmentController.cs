using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;

namespace ZimconBackOffice.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();
        // GET: Device
        public ActionResult Index(string id = null)
        {
            var shipment = _rest.GetShipmentOnVehicle(id);
            var vehicle = _rest.GetVehicles();
            var model = new ShipmentVehicleViewModel()
            {
                Shipments = shipment,
                Vehicles = vehicle
            };
            return View(model);
        }
    }

    public class ShipmentVehicleViewModel
    {
        public List<Shipment> Shipments { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}