using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Location;
using Microsoft.Bot.Builder.Location.Bing;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using ParkingBot.Models.Facebook.Facebook_user_channel_data;
using ParkingBot.Models.servicemodels;
using ParkingBot.Models.servicemodels.nearest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ParkingBot.Models.DialogControl
{
    [Serializable]
    public class RootDialog:IDialog<string>
    {
        private readonly string channelId;
        private RootObject param = new RootObject();
        private List<Location> ubicaciones = new List<Location>();
        string recomendacion = string.Empty;

        public RootDialog(string channel)
        {
            channelId = channel;
        }
#pragma warning disable CS1998
        public async Task StartAsync(IDialogContext context)
#pragma warning restore CS1998
        {
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            await this.MostrarMenuPrincipal(context);
        }

        private async Task MostrarMenuPrincipal(IDialogContext context)
        {
            var saludo = context.MakeMessage();
            saludo.Text = $"Hola {procesarNombre(context.Activity.From.Name)}!!!";
            await context.PostAsync(saludo);
            await Task.Delay(1500);
            saludo.Text = " Te ayudare a conseguir un parqueo..";
            //TODO: Insertar quick reply with location
            //await context.PostAsync(saludo);

            var apiKey = WebConfigurationManager.AppSettings["BingMapsApiKey"];
            LocationResourceManager x = new LocationResourceManager();
           
            var options = LocationOptions.UseNativeControl | LocationOptions.SkipFinalConfirmation | LocationOptions.SkipFavorites;
            var locationDialog = new LocationDialog(apiKey, channelId, saludo.Text, options );
            context.Call(locationDialog, this.ResumeAfterLocationDialogAsync);
            //context.Call(new DisponibilidadDialog(), this.DisponibilidadDialogResumeAfter);
        }

        private string procesarNombre(string name)
        {
            string nombre = "";
            var espacios = name.Split(' ').ToList();
            int nroespacios = espacios.Count;
            if (nroespacios <= 3)
            {
                nombre = espacios[0];
            }
            else
            {
                nombre = espacios[0] + " " + espacios[1];
            }
            return nombre;
        }

        private async Task ResumeAfterLocationDialogAsync(IDialogContext context, IAwaitable<Microsoft.Bot.Connector.Place> result)
        {
            try
            {
                var place = await result;
                if (place != null)
                {
                    var coordenadas = place.GetGeoCoordinates();
                    //await context.PostAsync("Coordenadas: " + coordenadas.Latitude + "," + coordenadas.Longitude);
                    var respuesta = context.MakeMessage();
                    respuesta.AttachmentLayout = "carousel";
                    var apiKey = WebConfigurationManager.AppSettings["BingMapsApiKey"];
                    LocationResourceManager nuevo = new LocationResourceManager();
                    LocationCardBuilder constructorcartas = new LocationCardBuilder(apiKey,nuevo);
                    double lat = (double)coordenadas.Latitude;
                    double lng = (double)coordenadas.Longitude;
                    ubicaciones = DevolverPosibles(lat, lng);
                    List<string> nombres = new List<string>();
                    foreach(var item in ubicaciones)
                    {
                        
                        string elemento = string.Empty;
                        elemento = item.Name;
                        nombres.Add(elemento);
                    }
                    recomendacion = DevolverMejorOpcion(lat, lng);
                    var cards= constructorcartas.CreateHeroCards(ubicaciones,locationNames:nombres);
                    respuesta.Attachments = cards.Select(c => c.ToAttachment()).ToList();

                    var recomienda = context.MakeMessage();
                    recomienda.SuggestedActions = MenuFact.DetallesQuickReplies(ubicaciones.Count);
                    recomienda.Text = recomendacion;
                    
                    await context.PostAsync(respuesta);
                    await context.PostAsync(recomienda);
                    param = DevolverRequest(lat, lng);
                    context.Wait(this.QuickReplySelectedAsync);
                }

                //context.Done<string>(null);
            }
            catch(Exception e)
            {
                await context.PostAsync("Error: " + e.Message);
            }
        }

        private async Task QuickReplySelectedAsync(IDialogContext context,IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            FBQuickReplyData elemento = message.ChannelData.ToObject<FBQuickReplyData>();
            var valorelegido = elemento.message.quick_reply.payload ?? "SOLOVOLVER";
            if (!string.IsNullOrEmpty(message.Text))
            {
                if (valorelegido == "SOLOVOLVER")
                {
                    context.Done<string>(null);
                }
                context.Call(new PlacaDialogDialog(param, valorelegido), PlacaDialogDialogResumeAfter);
            }

        }

        private async Task PlacaDialogDialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var respuesta = await result;
                if (respuesta == "SOLOVOLVER")
                {
                    var recomienda = context.MakeMessage();
                    recomienda.SuggestedActions = MenuFact.DetallesQuickReplies(ubicaciones.Count);
                    recomienda.Text = recomendacion;
                    await context.PostAsync(recomienda);
                    context.Wait(this.QuickReplySelectedAsync);
                }
                else
                {
                    if (respuesta == "EXITO")
                    {
                        context.Done<string>(null);
                    }
                }
            }
            catch(Exception e)
            {
                context.Fail(e);
            }
        }

        private List<Location> DevolverPosibles(double lat, double lng)
        {
            List<Location> retorno = new List<Location>();
            try
            {
                dtoCoordenadas nuevo = new dtoCoordenadas();
                nuevo.lat = lat;
                nuevo.lng = lng;
                Response resp = new Response();
                resp = new Services.RequestService().MakeHttpRequest("http://parkingwshack.gear.host/api/Parking/NearestParkings", "POST", nuevo);
                if (resp != null)
                {
                    if (resp.STATUS)
                    {
                        RootObject respuesta = JObject.FromObject(resp.DATA).ToObject<RootObject>();
                        foreach(var item in respuesta.list)
                        {
                            Location elemento = new Location();
                            elemento.Point = new GeocodePoint();
                            elemento.Point.Coordinates = new List<double>();
                            elemento.Name = item.place.nombre;
                            elemento.Point.Coordinates.Add(item.place.latitude);
                            elemento.Point.Coordinates.Add(item.place.longitude);
                            
                            retorno.Add(elemento);
                        }
                    }
                    return retorno;
                }
                throw new Exception("Unable to talk with service");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private RootObject DevolverRequest(double lat, double lng)
        {
            RootObject retorno = new RootObject();
            try
            {
                dtoCoordenadas nuevo = new dtoCoordenadas();
                nuevo.lat = lat;
                nuevo.lng = lng;
                Response resp = new Response();
                resp = new Services.RequestService().MakeHttpRequest("http://parkingwshack.gear.host/api/Parking/NearestParkings", "POST", nuevo);
                if (resp != null)
                {
                    if (resp.STATUS)
                    {
                        RootObject respuesta = JObject.FromObject(resp.DATA).ToObject<RootObject>();
                        retorno = respuesta;
                    }
                    return retorno;
                }
                throw new Exception("Unable to talk with service");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string DevolverMejorOpcion(double lat, double lng)
        {
            string retorno = string.Empty;
            try
            {
                dtoCoordenadas nuevo = new dtoCoordenadas();
                nuevo.lat = lat;
                nuevo.lng = lng;
                Response resp = new Response();
                resp = new Services.RequestService().MakeHttpRequest("http://parkingwshack.gear.host/api/Parking/NearestParkings", "POST", nuevo);
                if (resp != null)
                {
                    if (resp.STATUS)
                    {
                        RootObject respuesta = JObject.FromObject(resp.DATA).ToObject<RootObject>();
                        var tiempo = respuesta.list[0].travelTime;
                        var distancia = respuesta.list[0].travelDistance;
                        //var masbarato = respuesta.list.OrderBy(x => x.place.costo).FirstOrDefault();
                        retorno = "La opcion mas cercana (1) se encuentra a " + tiempo + " de tu destino y " + distancia + " de distancia";
                    }
                    return retorno;
                }
                throw new Exception("Unable to talk with service");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}