using System;
namespace Shipper.RestGateway.Model
{
    public class CurrentStatusByWaybill
    {
        public string id { get; set; }
        public string manifestReference { get; set; }
        public int wayBillNumber { get; set; }
        public string vehicleId { get; set; }
        public int statusId { get; set; }
        public DateTime createdDate { get; set; }
    }
}
