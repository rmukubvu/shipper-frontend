using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipper.RestGateway.Model
{
    public class ServerError
    {
        public DateTime timestamp { get; set; }
        public int status { get; set; }
        public string error { get; set; }
        public string message { get; set; }
        public string path { get; set; }
    }
}
