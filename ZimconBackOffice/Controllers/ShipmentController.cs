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
        public ActionResult Index()
        {
            var consignees = _rest.GetConsignee();
            var vehicle = _rest.GetVehicles();
            var model = new ShipmentVehicleViewModel()
            {
                Consignees = consignees,
                Vehicles = vehicle
            };
            return View(model);
        }

        public ActionResult ViewShipments(string id = null){
            var shipmentOnTruck = _rest.GetShipmentOnVehicle(id);
            return View(shipmentOnTruck); // would be great if these are cards with
            //each card having a small map with a marker to and status to show were it is
        }
    }

    public class ShipmentVehicleViewModel
    {
        public List<Consignee> Consignees { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}