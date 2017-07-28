using Azure_Bot_Generic_CSharp.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingBot.Models.Facebook.FacebookShareAction
{
    public class FBShareForwardAction
    {
        private Activity instance { get; set; }

        public FBShareForwardAction()
        {
            instance = new Activity();
            instance.ChannelData = new FacebookChannelData()
            {
                Attachment = new FacebookAttachment()
                {
                    Payload = new FacebookGenericTemplate()
                    {
                        Elements = new object[]
                        {
                            new FacebookGenericTemplateContent()
                            {
                                Buttons = new[]
                                {
                                    new FacebookShareButton()
                                }
                            }
                        }
                    }
                }
            };

        }

        public FBShareForwardAction(string titulo = "titulo", string subtitulo = "subtitulo", string urlimagen = "http://i.imgur.com/NrHdPli.png")
        {
            instance = new Activity();
            instance.ChannelData = new FacebookChannelData()
            {
                Attachment = new FacebookAttachment()
                {
                    Payload = new FacebookGenericTemplate()
                    {
                        Elements = new object[]
                        {
                            new FacebookGenericTemplateContent(titulo,subtitulo,urlimagen)
                            {
                                Buttons = new[]
                                {
                                    new FacebookShareButton()
                                }
                            }
                        }
                    }
                }
            };
        }

        /// <summary>
        /// IMPORTANTE: Asignar esto a ChannelData, no a Attachments
        /// </summary>
        /// <returns>Share button</returns>
        public object FBSBChannelData()
        {
            return instance.ChannelData;
        }
    }
}