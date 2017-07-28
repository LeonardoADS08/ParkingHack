using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking_Services.Models
{
    public class Respuestas
    {
        public bool STATUS { get; set; }
        public string MESSAGE { get; set; }
        public object DATA { get; set; }
    }
}