using System;

namespace Shipper.RestGateway.Model
{
    public class User
    {
        public string consigneeId { get; set; }
        public DateTime createdDate { get; set; }
        public string emailAddress { get; set; }
        public string id { get; set; }
        public string password { get; set; }
    }

    public class UserLogin
    {
        public bool error { get; set; }
        public string loginErrorMessage { get; set; }
        public User user { get; set; }
    }
}