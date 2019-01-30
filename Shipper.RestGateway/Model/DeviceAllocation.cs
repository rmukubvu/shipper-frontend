using System;

namespace Shipper.RestGateway.Model
{
    public class DeviceAllocation
    {
        public DateTime allocationDate { get; set; }
        public string deviceId { get; set; }
        public string id { get; set; }
        public string vehicleId { get; set; }
    }
}