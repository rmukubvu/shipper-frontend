using System;
namespace Shipper.RestGateway.Model
{
    public class StatusByWaybill
    {
        public string id { get; set; }
        public int wayBillNumber { get; set; }
        public string status { get; set; }
        public DateTime statusChangeDate { get; set; }
    }
}
