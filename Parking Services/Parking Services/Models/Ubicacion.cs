using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking_Services.Models
{
    public class Ubicacion
    {
        private double lat, lng;

        public double Lat { get => lat; set => lat = value; }
        public double Lng { get => lng; set => lng = value; }
    }
}