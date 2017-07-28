using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingBot.Models.servicemodels.nearest
{
    [Serializable]
    public class Encargado
    {
        public string _id { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public int __v { get; set; }
        public bool active { get; set; }
    }

    [Serializable]
    public class Place
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
    }

    [Serializable]
    public class List
    {
        public Place place { get; set; }
        public string travelDistance { get; set; }
        public string travelTime { get; set; }
        public int valueTravelDistance { get; set; }
        public int valueTravelTime { get; set; }
    }

    [Serializable]
    public class RootObject
    {
        public List<List> list { get; set; }
    }
}