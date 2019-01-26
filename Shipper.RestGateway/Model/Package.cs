using System;
namespace Shipper.RestGateway.Model
{
    public class Package
    {
        public string consigneeId { get; set; }
        public int destinationLatitude { get; set; }
        public int destinationLongitude { get; set; }
        public DateTime loadedDate { get; set; }
        public string manifestReference { get; set; }
        public int sourceLatitude { get; set; }
        public int sourceLongitude { get; set; }
        public string vehicleId { get; set; }
        public int wayBillNumber { get; set; }
    }
}
