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
        // GET: Device
        public ActionResult Index(string id = null)
        {
            var users = _rest.GetUser();
            var userView = getUserViewModel(users);

            UserConsigneeViewModel viewModel = new UserConsigneeViewModel
            {
                users = userView,
                consignees = _rest.GetConsignee()
            };

            return View(viewModel);
        }

        public List<UserViewModel> getUserViewModel(List<User> users)
        {
            List<UserViewModel> userView = new List<UserViewModel>();

            foreach (var user in users)
            {
                var consignee = _rest.GetConsigneeById(user.consigneeId);

                UserViewModel vm = new UserViewModel
                {
                    id = user.id,
                    consigneeId = user.consigneeId,
                    consignee = consignee,
                    firstName=user.firstName,
                    lastName=user.lastName,
                    emailAddress = user.emailAddress,
                    password = user.password,
                    createdDate = user.createdDate
                };
                userView.Add(vm);
            }

            return userView;
        }
    }

    public class UserConsigneeViewModel
    {
        public List<UserViewModel> users { get; set; }
        public List<Consignee> consignees { get; set; }
    }

}