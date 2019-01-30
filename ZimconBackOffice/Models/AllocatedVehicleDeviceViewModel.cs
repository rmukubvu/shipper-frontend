using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZimconBackOffice.Models
{
    public class AllocatedVehicleDeviceViewModel
    {
        public string allocationId { get; set; }
        public string deviceId { get; set; }
        public string deviceMake { get; set; }
        public string deviceModel { get; set; }
        public string vehicleId { get; set; }
        public string vehicleLicense { get; set; }
        public string vehicleMake { get; set; }
        public string vehicleModel { get; set; }
        public DateTime allocationDate { get; set; }

    }
}