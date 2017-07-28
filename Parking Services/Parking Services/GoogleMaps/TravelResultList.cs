using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Services.GoogleMaps
{
    public class TravelResultList
    {
        public List<TravelResult> list { get; set; }

        public TravelResultList()
        {
            list = new List<TravelResult>();
        }

        public void SortByTime() => list.Sort((a, b) => a.valueTravelTime.CompareTo(b.valueTravelTime));
        public void SortByDistance() => list.Sort((a, b) => a.valueTravelDistance.CompareTo(b.valueTravelDistance));
    }
}
