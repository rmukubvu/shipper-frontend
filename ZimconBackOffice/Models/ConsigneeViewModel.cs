using Shipper.RestGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZimconBackOffice.Models
{
    public class ConsigneeViewModel
    {
        public List<Consignee> Consignees { get; set; }
        public List<ConsigneeContactDetails> ConsigneeContacts { get; set; }
        public List<Country> Countries { get; set; }
        public string ConsingeeName { get; set; }
}
}