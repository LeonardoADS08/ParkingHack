using Microsoft.Bot.Connector;
using ParkingBot.Models.Facebook.FacebookShareAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingBot.Models
{
    public class CardsFact
    {
        /// <summary>
        /// Imagen enviada a facebook
        /// </summary>
        /// <param name="alt">si imagen no esta disponible</param>
        /// <param name="url">url de imagen pendiente base64</param>
        /// <param name="tap">accion al tocar imagen</param>
        /// <returns>Instancia de imagen</returns>
        public static CardImage GetCardImage(string alt, string url, CardAction tap = null)
        {
            return new CardImage(url: url, alt: alt, tap: tap);
        }


        /// <summary>
        /// Boton o acción
        /// </summary>
        /// <param name="tipo">tipo de accion que realizará el boton</param>
        /// <param name="titulo">Texto que se muestra en el boton</param>
        /// <param name="urlimagen">Si se quiere añadir imagen al boton</param>
        /// <param name="valor">El valor o accion que tomara el boton</param>
        /// <returns></returns>
        public static CardAction GetCardAction(string tipo, string titulo = null, string urlimagen = null, object valor = null, string texto = null, string textomuestra = null)
        {
            return new CardAction(type: tipo, title: titulo, image: urlimagen, value: valor, text: texto, displayText: textomuestra);
        }


        /// <summary>
        /// Combinatoria de elementos en un mensaje del canal
        /// </summary>
        /// <param name="title">titulo del mensaje</param>
        /// <param name="subtitle">subtitulo o descripcion</param>
        /// <param name="text">Texto adicional</param>
        /// <param name="cardImage">Imagen o imagenes</param>
        /// <param name="cardAction">boton</param>
        /// <param name="acciones">botones</param>
        /// <returns></returns>
        public static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction = null, List<CardAction> acciones = null)
        {
            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = (acciones == null) ? new List<CardAction>() { cardAction } : acciones,
            };

            return heroCard.ToAttachment();
        }

        /// <summary>
        /// Imagen más grande
        /// </summary>
        /// <param name="title">Titulo del mensaje con imagen mas grande</param>
        /// <param name="subtitle">subtitulo o descripcion adicional</param>
        /// <param name="text">Texto</param>
        /// <param name="cardImage">Imagen incluida</param>
        /// <param name="cardAction">Boton incluido</param>
        /// <returns></returns>
        public static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction = null, List<CardAction> acciones = null, CardAction ontouch = null)
        {
            var heroCard = new HeroCard
            {
                Tap = ontouch,
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = (acciones == null) ? new List<CardAction>() { cardAction } : acciones,
            };

            return heroCard.ToAttachment();
        }

        /// <summary>
        /// Plantilla de recibo de facebook o detalle de una compra/venta
        /// </summary>
        /// <param name="botones">Lista CardActions como en cualquier otra</param>
        /// <param name="hechos"></param>
        /// <param name="detalleItems"></param>
        /// <param name="totalAPagar"></param>
        /// <param name="impuesto"></param>
        /// <param name="titulo"></param>
        /// <param name="costoTransporte"></param>
        /// <param name="accionAlPresionar"></param>
        /// <returns></returns>
        public static Attachment GetReceiptCard(List<CardAction> botones = null, List<Fact> hechos = null, List<ReceiptItem> detalleItems = null,
            string totalAPagar = "", string impuesto = "", string titulo = "", string costoTransporte = "", CardAction accionAlPresionar = null)
        {
            ReceiptCard x = new ReceiptCard();
            var receiptCard = new ReceiptCard()
            {
                Buttons = botones,
                Facts = hechos,
                Items = detalleItems,
                Total = totalAPagar,
                Tax = impuesto,
                Title = titulo,
                Vat = costoTransporte,
                Tap = accionAlPresionar
            };


            return receiptCard.ToAttachment();
        }

        public static object getShareButton(string titulo = "", string subtitulo = "", string urlimagen = "http://i.imgur.com/NrHdPli.png")
        {
            FBShareForwardAction card = new FBShareForwardAction(titulo, subtitulo, urlimagen);
            return card.FBSBChannelData();
        }


    }
}