namespace Parking_Services.Models
{
    public class Encargado
    {
        public string _id { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public int __v { get; set; }
        public bool active { get; set; }

        public Encargado()
        {
            _id = "";
            nombre = "";
            telefono = "";
            __v = 0;
            active = true;
        }
    }
}