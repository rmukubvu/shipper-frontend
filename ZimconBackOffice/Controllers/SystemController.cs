using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Google.Maps;
using Google.Maps.Geocoding;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;


namespace ZimconBackOffice.Controllers
{
    public class SystemController : ApiController
    {
        private readonly RestCalls _service = new RestCalls();
        //TODO: remove user from admin
        #region gets
        [Route("api/login"),HttpGet]
        public UserLogin DoLogin(string userName, string password)
        {
            return _service.Login(userName, password);
        }     

        [Route("api/changePassword"), HttpGet]
        public User ChangePassword(string userName, string password)
        {
            return _service.ChangePassword(userName, password);
        }

        [Route("api/cargoOnVehicle"), HttpGet]
        public List<Shipment> GetCargoLoaded(string vehicleId)
        {
            return _service.GetShipmentOnVehicle(vehicleId);
        }

        [Route("api/revokeAdmin"), HttpGet]
        public RestResponse RemoveUserAsAdmin(string userName)
        {
            return _service.RemoveUserAsAdmin(userName);
        }

        [Route("api/recoverPassword"), HttpGet]
        public RestResponse RecoverPassword(string userName)
        {
            return _service.RecoverPassword(userName);
        }

        #endregion

        #region Posts

        [Route("api/user"), HttpPost]
        public string SaveUser(User model)
        {
            return _service.SaveUser(model);
        }

        [Route("api/consignee"), HttpPost]
        public string SaveConsignee(Consignee model)
        {
            return _service.SaveConsignee(model);
        }

        [Route("api/vehicle"), HttpPost]
        public string SaveVehicle(Vehicle model)
        {
            return _service.SaveVehicle(model);
        }

        [Route("api/devicealloc"), HttpPost]
        public string AllocateDevice(SmartDeviceAllocation model)
        {
            return _service.SaveSmartDeviceAllocation(model);
        }

        [Route("api/driver"), HttpPost]
        public string SaveDriver(Driver model)
        {
            return _service.SaveDriver(model);
        }

        [Route("api/driveralloc"), HttpPost]
        public string AllocateDriver(DriverAllocation model)
        {
            return _service.AllocateDriverToVehicle(model);
        }

        [Route("api/package"), HttpPost]
        public string AddPackageOntoVehicle(PackageViewModel model)
        {
            double sourceLat = 0;
            double sourceLon = 0;
            double destLat = 0;
            double destLon = 0;

            var request = new GeocodingRequest
            {
                Address = model.sourceAddress
            };
            var response = new GeocodingService().GetResponse(request);
            if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)
            {
                var result = response.Results.First();
                sourceLat = result.Geometry.Location.Latitude;
                sourceLon = result.Geometry.Location.Longitude;
            }

            request = new GeocodingRequest
            {
                Address = model.destinationAddress
            };

            response = new GeocodingService().GetResponse(request);
            if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)
            {
                var result = response.Results.First();
                destLat = result.Geometry.Location.Latitude;
                destLon = result.Geometry.Location.Longitude;
            }

            return _service.SavePackageOntoVehicle(model.getPackageFromViewModel(sourceLat, sourceLon, destLat, destLon));
        }

        [Route("api/updateUser"), HttpPost]
        public string UpdateUser(User model)
        {
            return _service.UpdateUser(model);
        }

        [Route("api/userAdmin"), HttpPost]
        public string UserAdmin(SystemAdmin model)
        {
            return _service.AddUserAsAdmin(model);
        }

        [Route("api/updateStatus"),HttpPost]
        public string SaveShipmentStatus(string vehicleId, string manifestReference, long waybill, int statusId)
        {
            return _service.SaveShipmentStatus(vehicleId, manifestReference, waybill, statusId);
        }

        #endregion
    }
}