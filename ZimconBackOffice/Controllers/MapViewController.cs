
using System.Collections.Generic;
using System.Web.Mvc;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;
using ZimconBackOffice.Models;

namespace ZimconBackOffice.Controllers
{
    public class MapViewController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();
        // GET: MapView
        public ActionResult Index(string id)
        {
            var history = _rest.GetVehicleLocationHistory(id);
            var model = GetCoordinates(history);
            return View(model);
        }

        private List<TruckCoordinates> GetCoordinates(List<VehicleLocationHistory> list)
        {
            var result = new List<TruckCoordinates>();
            foreach (var vehicleLocationHistory in list)
            {
                result.Add(new TruckCoordinates()
                {
                    lat = vehicleLocationHistory.latitude,
                    lng = vehicleLocationHistory.longitude
                });
            }
            return result;
        }
    }
}