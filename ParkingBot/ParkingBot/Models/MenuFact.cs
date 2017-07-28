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
                a.Actions.Add(CardsFact.GetCardAction(ActionTypes.PostBack, "Detalles de " + (i+1).ToString(), valor: "VER" + (i+1).ToString(), textomuestra: (i+1).ToString()));
            }
            return a;
        }

        public static SuggestedActions OpcionesQuickReplies()
        {
            SuggestedActions a = new SuggestedActions();
            a.Actions = new List<CardAction>() {
                CardsFact.GetCardAction(ActionTypes.PostBack,"SI",valor:"RESERVAR",textomuestra:"SI"),
                CardsFact.GetCardAction(ActionTypes.PostBack,"NO",valor:"EXITO",textomuestra:"NO"),
                CardsFact.GetCardAction(ActionTypes.PostBack,"Volver",valor:"SOLOVOLVER",textomuestra:"Volver")
            };
            return a;
        }
    }
}