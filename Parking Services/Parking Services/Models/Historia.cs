using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking_Services.Models
{
    public class Historia
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public Parqueo lugar { get; set; }

        public Historia()
        {
            id = -1;
            fecha = new DateTime();
            lugar = new Parqueo();
        }
    }
}