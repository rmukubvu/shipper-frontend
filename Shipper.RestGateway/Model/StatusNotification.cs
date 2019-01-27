using System;
namespace Shipper.RestGateway.Model
{
    public class StatusNotification
    {
        public DateTime createdDate { get; set; }
        public string id { get; set; }
        public string manifestReference { get; set; }
        public int statusId { get; set; }
        public string vehicleId { get; set; }
        public int wayBillNumber { get; set; }
    }
}
