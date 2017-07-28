using Microsoft.Bot.Builder.Dialogs;
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
            
        }
    }
}