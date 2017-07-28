using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace ParkingBot.Services
{
    public class ProxyConection
    {
        public static IWebProxy GetDefault()
        {
            var credentials = GetCredentials();

            if (credentials == null)
                return null;

            var address = ConfigurationManager.AppSettings["Proxy:Address"];

            if (address == null)
                return null;

            var proxy = new WebProxy(address, true, new String[0], credentials);

            return proxy;
        }

        public static ICredentials GetCredentials()
        {
            var domain = ConfigurationManager.AppSettings["Proxy:Domain"];
            var username = ConfigurationManager.AppSettings["Proxy:Username"];
            var password = ConfigurationManager.AppSettings["Proxy:Password"];

            if (domain == null || username == null || password == null)
                return null;

            return new NetworkCredential(username, password, domain);
        }
    }
}