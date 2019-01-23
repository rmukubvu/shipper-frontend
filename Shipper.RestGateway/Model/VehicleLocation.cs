using System;

namespace Shipper.RestGateway.Model
{
    public class VehicleLocation
    {
        public string deviceId { get; set; }
        public string id { get; set; }
        public double latitude { get; set; }
        public DateTime locationDateTime { get; set; }
        public double longitude { get; set; }
        public string vehicleId { get; set; }
    }
}
