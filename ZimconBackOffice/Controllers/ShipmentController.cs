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
            if (string.IsNullOrEmpty(id))
            {
                return View(new List<Shipment>());
            }
            var shipmentOnTruck = _rest.GetShipmentOnVehicle(id);
            return View(shipmentOnTruck); 
        }

        public ActionResult Search(string id)
        {
            var model = new SearchViewModel();
            if (string.IsNullOrEmpty(id))
            {
                model.IsError = true;
                model.ErrorMessage = "Waybill number is not valid";
            }
            else
            {
                try
                {
                    var shipment = _rest.GetShipmentByWayBill(int.Parse(id));
                    if (shipment != null)
                    {
                        var owner = _rest.GetConsigneeById(shipment.consigneeId);
                        var vehicle = _rest.GetVehicleById(shipment.vehicleId);
                        var status = _rest.GetDashboardStatusByWaybill(int.Parse(id));
                        var statuses = _rest.GetClearingStatuses();

                        model = new SearchViewModel()
                        {
                            Owner = owner,
                            Truck = vehicle,
                            Status = status,
                            Shipment = shipment,
                            IsError = false,
                            clearingStatuses = statuses
                        };
                    }
                    else
                    {
                        model.IsError = true;
                        model.ErrorMessage = "Search with Waybill number brought no results";
                    }
                }
                catch (Exception e)
                {
                    model.IsError = true;
                    model.ErrorMessage = e.Message;
                }
            }
            return View(model);
        }
    }

    public class SearchViewModel : ZimconBackOffice.Models.ModelError
    {
        public Consignee Owner { get; set; }
        public FullShipment Shipment { get; set; }
        public DashboardStatus Status { get; set; }
        public Vehicle Truck { get; set; }
        public List<ClearingStatus> clearingStatuses { get; set; }
    }

    public class ShipmentVehicleViewModel
    {
        public List<Consignee> Consignees { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}