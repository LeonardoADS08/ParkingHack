using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Parking_Services.Models;

namespace Parking_Services.GoogleMaps
{
    public class ParkingRequest
    {
        double initLat, initLng;
        List<Models.Parqueo> destination;

        public double InitLat { get => initLat; set => initLat = value; }
        public double InitLng { get => initLng; set => initLng = value; }
        public List<Parqueo> Destination { get => destination; set => destination = value; }

        public ParkingRequest(double initialLatitude, double initialLongitude, List<Models.Parqueo> destinations)
        {
            initLat = initialLatitude;
            initLng = initialLongitude;
            destination = destinations;
        }

        public TravelResultList Calculate(bool sortByTime = true)
        {
            // Lista de resultados
            TravelResultList result = new TravelResultList();

            // Por cada destino, se calcula la distancia y el tiempo.
            foreach (Models.Parqueo val in destination)
            {
                // Se verifica si esta disponible
                // Se verifica que este dentro de la hora de atención.
                if (!val.Disponibilidad || !val.HorarioValido()) continue;

                Google.Maps.DistanceMatrix.DistanceMatrixRequest distanceRequest = new Google.Maps.DistanceMatrix.DistanceMatrixRequest()
                {
                    WaypointsOrigin = new List<Google.Maps.Location> { new Google.Maps.LatLng(initLat, initLng) },
                    WaypointsDestination = new List<Google.Maps.Location> { new Google.Maps.LatLng(val.Latitud, val.Longitud) },
                    Sensor = false
                };

                try
                {
                    var response = new Google.Maps.DistanceMatrix.DistanceMatrixService().GetResponse(distanceRequest);
                    result.List.Add(
                        new TravelResult
                        (val,
                        response.Rows.First().Elements.First().distance.Text,
                        response.Rows.First().Elements.First().duration.Text,
                        Convert.ToInt32(response.Rows.First().Elements.First().distance.Value),
                        Convert.ToInt32(response.Rows.First().Elements.First().duration.Value)
                        ));
                }
                catch
                {
                    return null;
                }
            }

            // Se ordena por tiempo o distancia
            if (sortByTime) result.SortByTime();
            else result.SortByDistance();

            return result;
        }

    }
}