namespace Shipper.RestGateway.Model
{
    public class DashboardStatus
    {
        public int waybill { get; set; }
        public int progress { get; set; }
        public string label { get; set; }
        public string currentStatus { get; set; }
    }
}
