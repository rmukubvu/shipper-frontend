using System.Collections.Generic;
using System.Web.Http;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;


namespace ZimconBackOffice.Controllers
{
    public class SystemController : ApiController
    {
        private readonly RestCalls _service = new RestCalls();

        [Route("api/login")]
        [HttpGet]
        public UserLogin DoLogin(string userName, string password)
        {
            return _service.Login(userName, password);
        }

        [Route("api/getShipment")]
        [HttpGet]
        public WaybillShipment GetShipment(string waybill)
        {
            return  _service.GetShipment(waybill);
        }
        
        [Route("api/user")]
        [HttpPost]
        public string SaveUser(User model)
        {
            return _service.SaveUser(model);
        }

        [Route("api/consignee")]
        [HttpPost]
        public string SaveConsignee(Consignee model)
        {
            return _service.SaveConsignee(model);
        }

        [Route("api/vehicle")]
        [HttpPost]
        public string SaveVehicle(Vehicle model)
        {
            return _service.SaveVehicle(model);
        }

        [Route("api/devicealloc")]
        [HttpPost]
        public string AllocateDevice(SmartDeviceAllocation model)
        {
            return _service.SaveSmartDeviceAllocation(model);
        }

        [Route("api/driver")]
        [HttpPost]
        public string SaveDriver(Driver model)
        {
            return _service.SaveDriver(model);
        }

        [Route("api/driveralloc")]
        [HttpPost]
        public string AllocateDriver(DriverAllocation model)
        {
            return _service.AllocateDriverToVehicle(model);
        }


        [Route("api/shipment")]
        [HttpPost]
        public string saveShipment(Shipment model)
        {
            return _service.SaveShipment(model);
        }


        [Route("api/package")]
        [HttpPost]
        public string AddPackageOntoVehicle(Package model)
        {
            return _service.SavePackageOntoVehicle(model);
        }
    }
}