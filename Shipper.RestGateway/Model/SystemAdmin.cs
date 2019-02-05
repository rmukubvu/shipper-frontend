using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipper.RestGateway.Model
{
    public class SystemAdmin
    {
        public string id { get; set; }
        public bool isAdmin { get; set; }
        public string userId { get; set; }
    }
}
