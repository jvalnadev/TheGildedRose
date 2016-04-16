using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGildedRose.Models
{
    public class Status
    {
        public int code { get; set; }
        public string message { get; set; }

        public Status()
        {
            code = -1;
            message = "";
        }
        public Status(int code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}