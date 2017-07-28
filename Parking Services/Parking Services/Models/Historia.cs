using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking_Services.Models
{
    public class Historia
    {
        private int id;
        private DateTime fecha;
        private Parqueo lugar;

        public int Id { get => id; set => id = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public Parqueo Lugar { get => lugar; set => lugar = value; }

        public Historia()
        {
            id = -1;
            fecha = new DateTime();
            lugar = new Parqueo();
        }
    }
}