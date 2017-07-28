using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace Parking_Services.Controllers
{
    public class ParkingController : ApiController
    {
        public JObject NearestParkings(JObject rawOrigin)
        {
            Models.Respuestas res = new Models.Respuestas();
            try
            {
                Services.RequestService request = new Services.RequestService();
                
                string Response = request.MakeHttpRequestString("https://parkinghack.herokuapp.com/api/parkingspot");

                List<Models.Parqueo> destinations = JArray.Parse(Response).ToObject<List<Models.Parqueo>>();
                Debug.WriteLine(destinations.Count);

                Models.Ubicacion origin = rawOrigin.ToObject<Models.Ubicacion>();
                GoogleMaps.ParkingRequest parkingRequest = new GoogleMaps.ParkingRequest(origin.lat, origin.lng, destinations);
                var parkingResult = parkingRequest.Calculate();

                
                res.STATUS = true;
                res.MESSAGE = "OK";
                res.DATA = parkingResult;

                return JObject.FromObject(res);
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.MESSAGE = ex.Message;
                res.DATA = null;

                return JObject.FromObject(res);
            }
        }
    }
}
