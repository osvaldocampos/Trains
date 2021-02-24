using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;

namespace Trains.Utils
{
    public class RouteModelBuilder
    {
        private string _properties { get; set; }
        private RouteModelBuilder()
        { }

        public static RouteModelBuilder Get() => new RouteModelBuilder();

        public RouteModelBuilder WithChainOfProperties(string properties)
        {
            _properties = properties;
            return this;
        }

        public Result<RouteModel> Build()
        {
            try
            {
                _properties = _properties.Trim();
                return Result<RouteModel>.Ok(new RouteModel() { 
                    StartingPoint = _properties[0],
                    EndingPoint = _properties[1],
                    Distance = int.Parse(_properties[2].ToString())
                });
            }
            catch
            {
                return Result<RouteModel>.Fail($"Invalid arguments");
            }
        }
    }
}
