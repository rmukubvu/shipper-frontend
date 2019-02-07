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
    public class UserController : Controller
    {
        private readonly RestCalls _rest = new RestCalls();
        
        public ActionResult Index(string id = null)
        {
            var users = _rest.GetUsers();
            var userView = getUserViewModel(users);
            var viewModel = new UserConsigneeViewModel
            {
                users = userView,
                consignees = _rest.GetConsignee()
            };
            return View(viewModel);
        }

        public ActionResult Admin()
        {
            var users = _rest.GetUsers();
            //allow search of user
            return View(users);
        }

        public List<UserViewModel> getUserViewModel(List<User> users)
        {
            return (from user in users
                    let consignee = _rest.GetConsigneeById(user.consigneeId)
                    let userViewModel = new UserViewModel
                    {
                        id = user.id,
                        consigneeId = user.consigneeId,
                        consignee = consignee,
                        firstName = user.firstName,
                        lastName = user.lastName,
                        emailAddress = user.emailAddress,
                        password = user.password,
                        createdDate = user.createdDate
                    }
                    select userViewModel).ToList();
        }
    }

    public class UserConsigneeViewModel
    {
        public List<UserViewModel> users { get; set; }
        public List<Consignee> consignees { get; set; }
    }

}