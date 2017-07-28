using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace Parking_Services.Controllers
{
    public class SMSController : TwilioController
    {
        [HttpPost]
        public ActionResult Index()
        {
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("The Robots are coming! Head for the hills!");

            return TwiML(messagingResponse);
        }
        /*
        public TwiMLResult Index(SmsRequest request)
        {
            var response = new MessagingResponse();
            response.Message(
                $"Hey there {request.From}! " +
                "How 'bout those Seahawks?"
            );
            return TwiML(response);
        }*/
    }
}