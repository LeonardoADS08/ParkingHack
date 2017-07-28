using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking_Services.Models
{
    public class Parqueo
    {
        public string _id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public Encargado encargado { get; set; }
        public int costo { get; set; }
        public int __v { get; set; }
        public bool active { get; set; }
        public List<List<int>> horario { get; set; }

        public Parqueo()
        {
            _id = "";
            latitude = longitude = -1;
            nombre = "";
            nombre = "";
            costo = -1;
            horario = new List<List<int>>();
        }

        public bool HorarioValido()
        {
            try
            {
                int horaActual = DateTime.Now.Hour;
                foreach (List<int> val in horario)
                {
                    // Si hora actual es menor a hora de apertura o es mayor a hora de cierra, no esta disponible.
                    if (val[0] < horaActual || horaActual >= val[1]) return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}