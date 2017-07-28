using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using ParkingBot.Models.DialogControl;

namespace ParkingBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            
            if (activity.Type == ActivityTypes.Message)
            {
                //if (!string.IsNullOrEmpty(activity.Text) || act)
                //{
                //await activity.GetStateClient().BotState
                //.DeleteStateForUserWithHttpMessagesAsync(activity.ChannelId, activity.From.Id);

                //var client = new ConnectorClient(new Uri(activity.ServiceUrl));
                //var clearMsg = activity.CreateReply();
                //clearMsg.Text = $"Reseting everything for conversation: {activity.Conversation.Id}";
                //await client.Conversations.SendToConversationAsync(clearMsg);
                await Conversation.SendAsync(activity, () => new RootDialog(activity.ChannelId));
                //}
                //else
                //{
                //    var respuesta = activity.CreateReply("Lo siento, aun no puedo procesar archivos multimedia...");
                //    await connector.Conversations.SendToConversationAsync(respuesta);
                //}
            }
            else
            {
                Activity mensajeRespuesta = HandleSystemMessage(activity);
                if (mensajeRespuesta != null)
                {
                    await connector.Conversations.SendToConversationAsync(mensajeRespuesta);
                }
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        #region eventHandler
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
        #endregion
    }
}