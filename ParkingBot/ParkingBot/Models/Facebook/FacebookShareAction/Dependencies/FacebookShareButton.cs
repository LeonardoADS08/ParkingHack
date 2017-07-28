namespace Azure_Bot_Generic_CSharp.Models
{
    using Newtonsoft.Json;

    public class FacebookShareButton
    {
        public FacebookShareButton()
        {
            this.Type = "element_share";
        }

        [JsonProperty("type")]
        public string Type { get; set; }

        public override string ToString()
        {
            return $"type: {this.Type}";
        }

        public FacebookShareButton(string tipo,string titulo,string valor)
        {
            Type = tipo;
            Title = titulo;
            Value = valor;
        }
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}