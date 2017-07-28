using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Services.GoogleMaps
{
    public class TravelResultList
    {
        private List<TravelResult> list;

        public List<TravelResult> List { get => list; set => list = value; }

        public TravelResultList()
        {
            list = new List<TravelResult>();
        }
        public TravelResultList(List<TravelResult> data) => list = data;

        public void SortByTime() => list.Sort((a, b) => a.ValueTravelTime.CompareTo(b.ValueTravelTime));
        public void SortByDistance() => list.Sort((a, b) => a.ValueTravelDistance.CompareTo(b.ValueTravelDistance));
    }
}
