using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking_Services.Models
{
    public class Parqueo
    {
        private int id;
        private double latitud, longitud;
        private string nombre;
        private bool disponibilidad;
        private string tipo;
        private double costo;
        // Hora inicio, hora fin
        private List<Tuple<int, int>> horario;

        public int Id { get => id; set => id = value; }
        public double Longitud { get => longitud; set => longitud = value; }
        public double Latitud { get => latitud; set => latitud = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Disponibilidad { get => disponibilidad; set => disponibilidad = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public double Costo { get => costo; set => costo = value; }
        public List<Tuple<int, int>> Horario { get => horario; set => horario = value; }

        public Parqueo()
        {
            id = -1;
            latitud = longitud = -1;
            nombre = "";
            disponibilidad = false;
            nombre = "";
            costo = -1;
            horario = new List<Tuple<int, int>>();
        }

        public bool HorarioValido()
        {
            int horaActual = DateTime.Now.Hour;
            foreach (Tuple<int, int> val in horario)
            {
                // Si hora actual es menor a hora de apertura o es mayor a hora de cierra, no esta disponible.
                if (val.Item1 < horaActual || horaActual >= val.Item2) return false;
            }
            return true;
        }
    }
}