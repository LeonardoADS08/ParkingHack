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
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Threading.Tasks;

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

        

        public async Task<JObject> SendMessage(JObject data)
        {

            var client = new TwilioRestClient("ACe9efd8105eb0b71112e4e12511e569aa", "ab8f33a3539380f6a551d2ae2f9c876f");
            Models.SMS message = data.ToObject<Models.SMS>();
            Models.Respuestas result = new Models.Respuestas();
            var toPhoneNumber = new PhoneNumber(message.to);
            
            try
            {
                var sender = await MessageResource.CreateAsync(
                toPhoneNumber,
                from: new PhoneNumber(message.from),
                body: message.body,
                client: client);

                result.MESSAGE = "Message sent";
                result.STATUS = true;
                result.DATA = null;

                return JObject.FromObject(result);
            }
            catch (Exception ex)
            {
                result.MESSAGE = "Error";
                result.STATUS = false;
                result.DATA = null;

                return JObject.FromObject(result);
            }
        }
    }
}
