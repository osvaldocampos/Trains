using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    public class RouteModel
    {
        public char StartingPoint { get; set; }
        public char EndingPoint { get; set; }
        public int Distance { get; set; }

        public RouteModel() { }
    }
}
