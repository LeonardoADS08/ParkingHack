using ParkingBot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ParkingBot.Services
{
    public class RequestService
    {
        public System.Net.HttpWebResponse MakeHttpRequest(String url, String method)
        {
            var request = System.Net.WebRequest.Create(url);
            request.ContentLength = 0;
            request.Method = method;

            var response = (System.Net.HttpWebResponse)request.GetResponse();

            return response;
        }

        public System.Net.HttpWebResponse MakeHttpRequestWithProxy(String url, String contentType)
        {
            var request = System.Net.WebRequest.Create(url);

            request.Proxy = ProxyConection.GetDefault();
            request.ContentType = contentType;

            var response = (System.Net.HttpWebResponse)request.GetResponse();

            return response;
        }

        public Models.Response MakeHttpRequest(String url, String method = "GET", Object data = null)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = method;
                request.ContentType = "application/json; charset=utf-8";// urlcoded
                request.Expect = "application/json";
                request.PreAuthenticate = true;
                request.UseDefaultCredentials = true;

                if (data != null)
                {
                    using (Stream s = new MemoryStream())
                    {
                        var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        formatter.Serialize(s, data);
                    }
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(data);

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }


                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();
                    var myResponse = (Response)js.Deserialize(objText, typeof(Response));

                    return myResponse;
                }
            }
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            {
#pragma warning disable CS4014 // Ya que no se esperaba esta llamada, la ejecución del método actual continúa antes de que se complete la llamada
                Services.Mailing.SendEmail("i-jgutierrez@cotas.com", "Jorge", Newtonsoft.Json.JsonConvert.SerializeObject(e.Message, Newtonsoft.Json.Formatting.Indented), "service");
#pragma warning restore CS4014 // Ya que no se esperaba esta llamada, la ejecución del método actual continúa antes de que se complete la llamada

                return null;
            }

        }

        public Response MakeHttpRequestWithProxy(String url, String method = "GET", Object data = null)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                request.Proxy = ProxyConection.GetDefault();
                request.Method = method;
                request.ContentType = "application/json; charset=utf-8";
                request.Expect = "application/json";
                request.PreAuthenticate = true;
                request.UseDefaultCredentials = true;

                if (data != null)
                {
                    using (Stream s = new MemoryStream())
                    {
                        var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        formatter.Serialize(s, data);
                    }
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(data);

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }


                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();
                    var myResponse = (Response)js.Deserialize(objText, typeof(Response));

                    return myResponse;
                }
            }
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            {
                return null;
            }

        }

    }
}