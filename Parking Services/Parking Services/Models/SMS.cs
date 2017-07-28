using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking_Services.Models
{
    public class SMS
    {
        public string to { get; set; }
        public string from { get; set; }
        public string body { get; set; }

        public SMS()
        {
            to = "";
            from = "";
            body = "";
        }
    }
}