using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingBot.Models
{
    public class MenuFact
    {
        public static SuggestedActions DetallesQuickReplies(int opciones)
        {
            SuggestedActions a = new SuggestedActions();
            a.Actions = new List<CardAction>();
            for (int i = 0; i < opciones; i++)
            {
                a.Actions.Add(CardsFact.GetCardAction(ActionTypes.ImBack, "Detalles de " + (i+1).ToString(), valor: "VER" + (i+1).ToString(), textomuestra: (i+1).ToString()));
            }
            return a;
        }
    }
}