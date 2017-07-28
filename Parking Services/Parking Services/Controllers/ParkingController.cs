using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Parking_Services.Controllers
{
    public class ParkingController : ApiController
    {
        public JObject NearestParkings(JObject origin)
        {
            
            return new JObject();
        }
    }
}
