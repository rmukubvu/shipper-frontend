using System;

namespace Shipper.RestGateway.Model
{
    public class SmartDeviceAllocation
    {
        public string id { get; set; }
        public string deviceId { get; set; }
        public string vehicleId { get; set; }
        public DateTime allocationDate { get; set; }
    }

    public class AllocatedDevice
    {
        public SmartDeviceAllocation smartDeviceAlloc { get; set; }
        public Vehicle allocatedVehicle { get; set; }
    }
}
