using Newtonsoft.Json.Linq;
using Parking_Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Parking_Services.Services
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

        public Models.Respuestas MakeHttpRequest(String url, String method = "GET", Object data = null)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = method;
                request.ContentType = "urlcoded";// urlcoded  application/json; charset=utf-8
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
                    var myResponse = (Respuestas)js.Deserialize(objText, typeof(Respuestas));

                    return myResponse;
                }
            }
#pragma warning disable CS0168 // La variable está declarada pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable está declarada pero nunca se usa
            {
#pragma warning disable CS4014 // Ya que no se esperaba esta llamada, la ejecución del método actual continúa antes de que se complete la llamada
#pragma warning restore CS4014 // Ya que no se esperaba esta llamada, la ejecución del método actual continúa antes de que se complete la llamada

                return null;
            }

        }

        public string MakeHttpRequestString(String uri, String Method = "GET", Object data = null)
        {
            try
            {


                string content;

                HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                req.KeepAlive = false;
                req.Method = Method.ToUpper();

                if (("POST,PUT").Split(',').Contains(Method.ToUpper()))
                {
                    Console.WriteLine("Enter XML FilePath:");
                    string FilePath = Console.ReadLine();
                    content = (File.OpenText(@FilePath)).ReadToEnd();

                    byte[] buffer = Encoding.ASCII.GetBytes(content);
                    req.ContentLength = buffer.Length;
                    req.ContentType = "text/xml";
                    Stream PostData = req.GetRequestStream();
                    PostData.Write(buffer, 0, buffer.Length);
                    PostData.Close();
                }

                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

                Encoding enc = System.Text.Encoding.GetEncoding(1252);
                StreamReader loResponseStream =
                new StreamReader(resp.GetResponseStream(), enc);

                string Response = loResponseStream.ReadToEnd();

                loResponseStream.Close();
                resp.Close();

                return Response;
            }
            catch
            {
                return null;
            }
        }

        public Respuestas MakeHttpRequestWithProxy(String url, String method = "GET", Object data = null)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                request.Proxy = ProxyConection.GetDefault();
                request.Method = method;
                request.ContentType = "urlcoded"; //application/json; charset=utf-8
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
                    var myResponse = (Respuestas)js.Deserialize(objText, typeof(Respuestas));

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