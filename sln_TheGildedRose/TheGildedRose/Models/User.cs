using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGildedRose.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string City { get; set; }

        public string APIAccessToken { get; set; }
        public string HashedPassword { get; set; }
        public DateTime TokenIssueDate { get; set; }

        public User()
        {
            this.Username = "";
            this.Password = "";
            this.City = "";
            this.APIAccessToken = "";
            this.HashedPassword = "";
            TokenIssueDate = new DateTime(1000, 1, 1);
        }
    }
}