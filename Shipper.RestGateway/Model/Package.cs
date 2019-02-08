using System;
namespace Shipper.RestGateway.Model
{
    public class Package
    {
        public string consigneeId { get; set; }
        public double destinationLatitude { get; set; }
        public double destinationLongitude { get; set; }
        public DateTime loadedDate { get; set; }
        public string manifestReference { get; set; }
        public double sourceLatitude { get; set; }
        public double sourceLongitude { get; set; }
        public string vehicleId { get; set; }
        public string contents { get; set; }
        public int wayBillNumber { get; set; }
    }

    public class PackageViewModel
    {
        public string consigneeId { get; set; }
        public string destinationAddress { get; set; }
        public DateTime loadedDate { get; set; }
        public string manifestReference { get; set; }
        public string sourceAddress { get; set; }
        public string vehicleId { get; set; }
        public int wayBillNumber { get; set; }
        public string contents { get; set; }

        public Package getPackageFromViewModel(double dLat, double dLon, double sLat, double sLon)
        {
            return new Package()
            {
                consigneeId = this.consigneeId,
                destinationLatitude = dLat,
                destinationLongitude = dLon,
                loadedDate = DateTime.Now,
                manifestReference = this.manifestReference,
                sourceLatitude = sLat,
                sourceLongitude = sLon,
                vehicleId = this.vehicleId,
                wayBillNumber = wayBillNumber,
                contents = this.contents
            };
        }
    }
}
