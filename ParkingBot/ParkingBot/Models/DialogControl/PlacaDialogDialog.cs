using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using ParkingBot.Models.Facebook.Facebook_user_channel_data;
using ParkingBot.Models.servicemodels.nearest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ParkingBot.Models.DialogControl
{
    [Serializable]
    public class PlacaDialogDialog:IDialog<string> //ahorita lo cambio JOBEM
    {
        private readonly RootObject opciones = new RootObject();
        private readonly string OpcionElegida = string.Empty;
        public PlacaDialogDialog(RootObject op,string opdet)
        {
            opciones = op;
            OpcionElegida = opdet.Substring(3);
        }

        public async Task StartAsync(IDialogContext context)
        {
            var detalles = context.MakeMessage();
            var elemento = opciones.list[Convert.ToInt32(OpcionElegida) - 1];
            if (elemento.place.active)
            {
                await context.PostAsync("Parqueo disponible");
                await context.PostAsync("Precio: " + elemento.place.costo.ToString() + "Bs. por hora");
                detalles.Text = "Desea pedir una reserva?";
                detalles.SuggestedActions = MenuFact.OpcionesQuickReplies();
                await context.PostAsync(detalles);
                context.Wait(this.OpcionElegidaAsync);
            }
        }

        private async Task OpcionElegidaAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            FBQuickReplyData elemento = message.ChannelData.ToObject<FBQuickReplyData>();
            var valorelegido = elemento.message.quick_reply.payload ?? "SOLOVOLVER";
            await this.AccionOpcion(context, valorelegido);
        }

        private async Task AccionOpcion(IDialogContext context, string valorelegido)
        {
            if (valorelegido == "RESERVAR")
            {
                context.Call(new SolicitarPlacaDialog(), this.SolicitarPlacaResumeAfter);
            }
            else if (valorelegido == "EXITO")
            {
                context.Done<string>("EXITO");
            }
            else if (valorelegido == "SOLOVOLVER")
            {
                context.Done<string>("SOLOVOLVER");
            }
        }
        private async Task SolicitarPlacaResumeAfter(IDialogContext context,IAwaitable<string> result)
        {
            try
            {
                string placa = await result;
                await context.PostAsync("Solicitar placa no implementado aun: " + placa);
                context.Done<string>("EXITO");
            }
            catch(Exception e)
            {
                context.Fail(e);
            }
        }
    }
}