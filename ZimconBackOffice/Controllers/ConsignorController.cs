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
            //var consignee = _rest.GetConsignee();
            //var countries = _rest.GetCountries();
            //List<ConsigneeContactDetails> contacts = null;
            ////Consignee consingee = null;

            //if (id != null)
            //{
            //    //consignee = _rest.GetConsigneeById(id);

            //    contacts = _rest.GetConsigneeContactDetails(id);
            //}


            //ConsigneeViewModel consigneeViewModel = new ConsigneeViewModel
            //{
            //    Consignees = consignee,
            //    ConsigneeContacts = contacts,
            //    Countries = countries
            //};

            //return View(consigneeViewModel);
            return View(consignor);
        }

        //public ConsigneeViewModel getConsigneeDetails(string consigneeId)
        //{
        //    var viewModel = new ConsigneeViewModel
        //    {
        //        Consignees = _rest.GetConsignee(),
        //        ConsigneeContacts = _rest.GetConsigneeContactDetails(consigneeId)
        //    };
        //    return viewModel;
        //}
    }


}