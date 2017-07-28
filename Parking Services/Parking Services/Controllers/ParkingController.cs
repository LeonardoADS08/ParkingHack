using Newtonsoft.Json;
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
        public JObject NearestParkings(JObject rawOrigin, JObject rawDestinations)
        {
            try
            {
                List<Models.Parqueo> destinations = JArray.Parse(JsonConvert.SerializeObject(rawDestinations)).ToObject<List<Models.Parqueo>>();
                Models.Ubicacion origin = JArray.Parse(JsonConvert.SerializeObject(rawOrigin)).ToObject<Models.Ubicacion>();
                GoogleMaps.ParkingRequest request = new GoogleMaps.ParkingRequest(origin.Lat, origin.Lng, destinations);
                var result = request.Calculate();
            }
            catch
            {
                return null;
            }


            return new JObject();
        }
    }
}
