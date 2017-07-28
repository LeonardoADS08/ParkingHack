using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Parking_Services.Models;

namespace Parking_Services.GoogleMaps
{
    public class TravelResult
    {
        // informacion de parqueo, distancia, tiempo
        private Parqueo place;
        private string textTravelDistance, textTravelTime;
        private int valueTravelDistance, valueTravelTime;

        public TravelResult()
        {
            place = new Parqueo();
            textTravelDistance = "";
            textTravelTime = "";
            valueTravelDistance = 0;
            valueTravelTime = 0;

        }

        public TravelResult(Parqueo place, string distance, string time, int valueDistance, int valueTime)
        {
            this.place = place;
            textTravelDistance = distance;
            textTravelTime = time;
            valueTravelDistance = valueDistance;
            valueTravelTime = valueTime;
        }

        public Parqueo Place { get => place; set => place = value; }
        public string TravelDistance { get => textTravelDistance; set => textTravelDistance = value; }
        public string TravelTime { get => textTravelTime; set => textTravelTime = value; }
        public int ValueTravelDistance { get => valueTravelDistance; set => valueTravelDistance = value; }
        public int ValueTravelTime { get => valueTravelTime; set => valueTravelTime = value; }
    }
}