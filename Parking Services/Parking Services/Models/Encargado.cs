namespace Parking_Services.Models
{
    public class Encargado
    {
        private int id;
        private string nombre;
        private string telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
    }
}