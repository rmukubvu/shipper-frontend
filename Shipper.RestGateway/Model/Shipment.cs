using System.Collections.Generic;

namespace Shipper.RestGateway.Model
{
    public class Shipment
    {
        public string consignee { get; set; }
        public string manifestReference { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string status { get; set; }
        public long wayBill { get; set; }
        public IList<ContactDetail> contactDetails { get; set; }
    }

    public class ContactDetail
    {
        public string consigneeId { get; set; }
        public string name { get; set; }
        public string telephone { get; set; }
        public string emailAddress { get; set; }
        public string countryCode { get; set; }
    }
}
