namespace Shipper.RestGateway.Model
{
    public class WaybillShipment
    {
        public string id { get; set; }
        public string consigneeId { get; set; }
        public string destinationLatitude { get; set; }
        public string destinationLongitude{ get; set; }
        public string loadedDate { get; set; }
        public string manifestReference { get; set; }
        public string sourceLatitude { get; set; }
        public string vehicleId { get; set; }
        public string wayBillNumber { get; set; }
    }
}