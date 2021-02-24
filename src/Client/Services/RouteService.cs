using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trains.Models;

namespace Trains.Services
{
    public class RouteService
    {
        private List<RouteModel> _routes;
        public RouteService(List<RouteModel> routes)
        {
            _routes = routes;
        }

        public List<TripModel> NumberOfTrips(char from, char to)
        {
            var startingPoints = _routes.Where(x => x.StartingPoint == from).ToList();

            var twoStops = from firstStop in startingPoints
                           join secondStop in _routes.Where(x => x.EndingPoint == to)
                                on firstStop.EndingPoint equals secondStop.StartingPoint                           
                           select new TripModel(){ NumberOfStops = 2, Routes = new List<RouteModel>() { firstStop, secondStop }};

            var threeStops = from firstStop in startingPoints
                             join secondStop in _routes
                                  on firstStop.EndingPoint equals secondStop.StartingPoint
                             join thirdStop in _routes.Where(x => x.EndingPoint == to)
                                  on secondStop.EndingPoint equals thirdStop.StartingPoint
                             select new TripModel() { NumberOfStops = 3, Routes = new List<RouteModel>() { firstStop, secondStop, thirdStop } };

            var fourStops = from firstStop in startingPoints
                            join secondStop in _routes
                                 on firstStop.EndingPoint equals secondStop.StartingPoint
                            join thirdStop in _routes
                                 on secondStop.EndingPoint equals thirdStop.StartingPoint
                            join fourthStop in _routes.Where(x => x.EndingPoint == to)
                                 on thirdStop.EndingPoint equals fourthStop.StartingPoint
                            select new TripModel() { NumberOfStops = 4, Routes = new List<RouteModel>() { firstStop, secondStop, thirdStop, fourthStop } };

            var trips = new List<TripModel>();
            trips.AddRange(twoStops);
            trips.AddRange(threeStops);
            trips.AddRange(fourStops);
            return trips;
        }

        public int ShortestDistance(char from, char to)
        {
           List<int> totalTripDistances = new List<int>();
            NumberOfTrips(from, to)
                .ToList()
                .ForEach(x => {
                    int totalDistance = x.Routes.Select(y => y.Distance).Sum();
                    totalTripDistances.Add(totalDistance);                    
                });

            return totalTripDistances.OrderBy(x => x).First();
        }

        public Result<int> EvaluateDistance(string routeToEvaluate)
        {
            List<string> points = routeToEvaluate.Split("-").ToList();
            List<string> routesToSearch = new List<string>();
            int distance = 0;

            for (int i = 0; i < points.Count() - 1; i++)
            {
                routesToSearch.Add(points[i] + points[i + 1]);
            }

            foreach (var lookup in routesToSearch)
            {
                var routeAsCharArray = lookup.ToCharArray();
                var existingRoute = _routes.SingleOrDefault(y => y.StartingPoint == routeAsCharArray[0] && y.EndingPoint == routeAsCharArray[1]);

                if (existingRoute == null)
                {
                    return Result<int>.Fail("NO SUCH ROUTE");
                }

                distance += existingRoute.Distance;
            }

            return Result<int>.Ok(distance);
        }
    }
}
