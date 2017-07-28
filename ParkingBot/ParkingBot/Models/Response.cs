using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingBot.Models
{
    public class Response
    {
        public string MESSAGE { get; set; }
        public bool STATUS { get; set; }
        public object DATA { get; set; }


        public Response()
        {
            MESSAGE = "If you see this, then I've failed miserably...";
            STATUS = false;
        }
    }
}