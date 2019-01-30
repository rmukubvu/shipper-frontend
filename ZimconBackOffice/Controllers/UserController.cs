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

            return View(userView);
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
                    emailAddress = user.emailAddress,
                    password = user.password,
                    createdDate = user.createdDate
                };
                userView.Add(vm);
            }

            return userView;
        }
    }

    //public class UserViewModel
    //{
    //    public string id { get; set; }
    //    public string consigneeId { get; set; }
    //    public Consignee consignee { get; set; }
    //    public string emailAddress { get; set; }
    //    public string password { get; set; }
    //    public DateTime createdDate { get; set; }
    //}
}