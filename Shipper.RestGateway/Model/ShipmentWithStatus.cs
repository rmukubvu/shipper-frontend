using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipper.RestGateway.Model
{
    public class ShipmentWithStatus
    {
        public FullShipment shipment { get; set; }
        public DashboardStatus dashboardStatus { get; set; }
    }

    public class FullShipment
    {
        public string id { get; set; }
        public string vehicleId { get; set; }
        public string manifestReference { get; set; }
        public int wayBillNumber { get; set; }
        public string consigneeId { get; set; }
        public double sourceLatitude { get; set; }
        public double sourceLongitude { get; set; }
        public double destinationLatitude { get; set; }
        public double destinationLongitude { get; set; }
        public string contents { get; set; }
        public DateTime loadedDate { get; set; }
    }
}
