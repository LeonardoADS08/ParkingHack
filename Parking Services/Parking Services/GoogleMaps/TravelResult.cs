using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Parking_Services.Models;

namespace Parking_Services.GoogleMaps
{
    public class TravelResult
    {

        public TravelResult()
        {
            place = new Parqueo();
            travelDistance = "";
            travelTime = "";
            valueTravelDistance = 0;
            valueTravelTime = 0;

        }

        public TravelResult(Parqueo place, string distance, string time, int valueDistance, int valueTime)
        {
            this.place = place;
            travelDistance = distance;
            travelTime = time;
            valueTravelDistance = valueDistance;
            valueTravelTime = valueTime;
        }

        public Parqueo place { get; set; }
        public string travelDistance { get; set; }
        public string travelTime { get; set; }
        public int valueTravelDistance { get; set; }
        public int valueTravelTime { get; set; }
    }
}