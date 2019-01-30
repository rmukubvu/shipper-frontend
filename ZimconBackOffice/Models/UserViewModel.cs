using Shipper.RestGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZimconBackOffice.Models
{
    public class UserViewModel
    {
        public string allocationId { get; set; }
        public string id { get; set; }
        public string consigneeId { get; set; }
        public Consignee consignee { get; set; }
        public string emailAddress { get; set; }
        public string password { get; set; }
        public DateTime createdDate { get; set; }
    }
}