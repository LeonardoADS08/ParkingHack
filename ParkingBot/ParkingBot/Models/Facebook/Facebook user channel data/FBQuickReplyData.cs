using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingBot.Models.Facebook.Facebook_user_channel_data
{
    public class Sender
    {
        public string id { get; set; }
    }

    public class Recipient
    {
        public string id { get; set; }
    }

    public class QuickReply
    {
        public string payload { get; set; }
    }

    public class Message
    {
        public string mid { get; set; }
        public int seq { get; set; }
        public string text { get; set; }
        public bool is_echo { get; set; }
        public QuickReply quick_reply { get; set; }
    }

    public class FBQuickReplyData
    {
        public Sender sender { get; set; }
        public Recipient recipient { get; set; }
        public long timestamp { get; set; }
        public Message message { get; set; }
    }

}