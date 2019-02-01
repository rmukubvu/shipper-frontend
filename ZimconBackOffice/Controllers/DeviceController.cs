using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;

namespace ZimconBackOffice.Controllers
{
    public class DeviceController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();
        // GET: Device
        public ActionResult Index(string id = null)
        {
            var devices = _rest.GetSmartDevices();
            var vehicles = _rest.GetVehicles();
            var allocations = _rest.GetSmartDeviceAllocation();

            var deviceAllocationsView = GetAllocatedSmartDevicesView(allocations);

            var model = new DeviceAllocationViewModel()
            {
                Devices = devices,
                Vehicles = vehicles,
                Allocations = deviceAllocationsView
            };
            return View(model);
        }

        public List<Models.AllocatedVehicleDeviceViewModel> GetAllocatedSmartDevicesView(List<DeviceAllocation> allocations)
        {

            List<Models.AllocatedVehicleDeviceViewModel> allocatedDevices = new List<Models.AllocatedVehicleDeviceViewModel>();

            if (allocations == null)
                return null;

            foreach (var allocate in allocations)
            {
                var vehicle = _rest.GetVehicleById(allocate.vehicleId);
                var device = _rest.GetDeviceById(allocate.deviceId);

                if (vehicle != null && device != null)
                {
                    Models.AllocatedVehicleDeviceViewModel vm = new Models.AllocatedVehicleDeviceViewModel
                    {
                        allocationDate = allocate.allocationDate,
                        allocationId = allocate.id,
                        deviceId = allocate.deviceId,
                        vehicleId = vehicle.id,
                        vehicleLicense = vehicle.licenseId,
                        vehicleMake = vehicle.make,
                        vehicleModel = vehicle.model,
                        deviceMake = device.make,
                        deviceModel = device.model
                    };
                    allocatedDevices.Add(vm);
                }
            }

            return allocatedDevices;
        }
    }

    public class DeviceAllocationViewModel
    {
        public List<SmartDevice> Devices { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Models.AllocatedVehicleDeviceViewModel> Allocations { get; set; }
    }

}