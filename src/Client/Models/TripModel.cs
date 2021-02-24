using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    public class TripModel
    {
        public int NumberOfTrips { get; set; }
        public int NumberOfStops { get; set; }
        public List<RouteModel> Routes { get; set; }

        public TripModel()
        { 
        }
    }
}
