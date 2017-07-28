using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking_Services.Models
{
    public class Usuario
    {
        public string _id { get; set; }
        public string nombre { get; set; }
        public string idFacebook { get; set; }
        public string placa { get; set; }
        public string telefono { get; set; }
        public int __v { get; set; }
        public bool active { get; set; }

        public Usuario()
        {
            _id = "";
            nombre = "";
            idFacebook = "";
            placa = "";
            telefono = "";
            __v = 0;
            active = true;
        }
    }
}