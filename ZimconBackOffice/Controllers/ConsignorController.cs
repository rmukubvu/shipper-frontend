using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipper.RestGateway.Model;
using Shipper.RestGateway.RestClients;
using ZimconBackOffice.Models;

namespace ZimconBackOffice.Controllers
{
    public class ConsignorController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();
        // GET: Consignor
        public ActionResult Index(string id = null)
        {
            var consignor= _rest.GetConsignor();            
            return View(consignor);
        }       
    }


}