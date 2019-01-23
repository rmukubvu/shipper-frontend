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
    }
}