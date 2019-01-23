using System;

namespace Shipper.RestGateway.Model
{
    public class DriverAllocation
    {
        public DateTime allocatedEndDate { get; set; }
        public DateTime allocatedStartDate { get; set; }
        public string driverId { get; set; }
        public string id { get; set; }
        public string vehicleId { get; set; }
    }
}