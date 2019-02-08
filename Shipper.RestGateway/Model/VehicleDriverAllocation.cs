using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipper.RestGateway.Model
{
    public class VehicleDriverAllocation
    {
        public bool hasDriver { get; set; }
        public string driverId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string telephone { get; set; }
        public string vehicleId { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string licenseId { get; set; }
    }
}
