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
    public class SolicitarPlacaDialog:IDialog<string>
    {
        private int intentos = 3;
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Por favor, ingrese el numero de su placa para gestionar su reserva:");
            context.Wait(this.PlacaReceivedAsync);
        }

        private async Task PlacaReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if (!string.IsNullOrEmpty(message.Text))
            {
                context.Done(message.Text);
            }
            else
            {
                --intentos;
                if (intentos > 0)
                {
                    await context.PostAsync("No se recibio input valido");
                    context.Wait(this.PlacaReceivedAsync);
                }
                else
                {
                    context.Fail(new Exception("Intentos fallidos"));
                }
            }
        }
    }
}