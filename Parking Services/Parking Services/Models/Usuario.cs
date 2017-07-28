using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking_Services.Models
{
    public class Usuario
    {
        private int id;
        private int idFacebook;
        private string placa;
        private string telefono;
        private string nombre;

        public int Id { get => id; set => id = value; }
        public int IdFacebook { get => idFacebook; set => idFacebook = value; }
        public string Placa { get => placa; set => placa = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public Usuario()
        {
            id = -1;
            idFacebook = -1;
            placa = "";
            telefono = "";
            nombre = "";
        }
    }
}