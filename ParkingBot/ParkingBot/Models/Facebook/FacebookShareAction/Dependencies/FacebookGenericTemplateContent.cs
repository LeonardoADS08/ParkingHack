namespace Azure_Bot_Generic_CSharp.Models
{
    using Newtonsoft.Json;

    public class FacebookGenericTemplateContent
    {
        public FacebookGenericTemplateContent()
        {
            this.Title = "This is a title.";
            this.Subtitle = "This is a subtitle.";
            this.ImageUrl = "http://i.imgur.com/NrHdPli.png";
        }

        /// <summary>
        /// Similar a una HeroCard
        /// </summary>
        /// <param name="titulo">Titulo del elemento</param>
        /// <param name="subtitulo">Subtitulo del elemento</param>
        /// <param name="urlimagen">Imagen URL</param>
        public FacebookGenericTemplateContent(string titulo="titulo",string subtitulo="subtitulo",string urlimagen= "http://i.imgur.com/NrHdPli.png")
        {
            this.Title = titulo;
            this.Subtitle = subtitulo;
            this.ImageUrl = urlimagen;
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("buttons")]
        public object[] Buttons { get; set; }
    }
}