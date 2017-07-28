using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ParkingBot.Models.DialogControl
{
    [Serializable]
    public class RootDialog:IDialog<object>
    {
        public Response resp = new Response();
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
            saludo.Text = " Te ayudare a conseguir un parqueo. Enviame la ubicacion de tu destino";
            //TODO: Insertar quick reply with location
            await context.PostAsync(saludo);
            context.Call(new DisponibilidadDialog(), this.DisponibilidadDialogResumeAfter);
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

        private async Task DisponibilidadDialogResumeAfter(IDialogContext context, IAwaitable<OPControls.> result)
        {

        }
    }
}