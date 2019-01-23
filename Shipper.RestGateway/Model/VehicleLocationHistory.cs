using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipper.RestGateway.Model
{
    public class VehicleLocationHistory
    {
        public string id { get; set; }
        public string deviceId { get; set; }
        public string vehicleId { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime locationDateTime { get; set; }
    }
}
