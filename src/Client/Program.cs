using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Trains.DependencyResolution;
using Trains.Models;
using Trains.Services;
using Trains.Utils;

namespace Trains
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = ServicesRegistry
                .Register(serviceCollection)
                .BuildServiceProvider();

            var distances = new List<string>()
            {
                {  "A-B-C" },
                {  "A-D" },
                {  "A-D-C" },
                {  "A-E-B-C-D" },
                {  "A-E-D" }
            };

            Console.WriteLine("Enter the list of routes eg.(AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7):");
            string readLine = Console.ReadLine();
            var routeCodes = readLine.Split(",");
            List<RouteModel> routes = new List<RouteModel>();
            routeCodes.ToList().ForEach(x => {
                var result = RouteModelBuilder.Get()
                    .WithChainOfProperties(x)
                    .Build();

                if (result.IsSuccess)
                {
                    routes.Add(result.Value);
                }
            });

            var service = new RouteService(routes);

            List<Result<int>> outputs = new List<Result<int>>();

            for (int i = 0; i < distances.Count; i++)
            {
                var result = service.EvaluateDistance(distances[i]);

                if (result.IsSuccess)
                {
                    Console.WriteLine($"Output #{ i + 1}: { result.Value }");
                }
                else
                {
                    Console.WriteLine($"Output #{ i + 1}: { result.Message }");
                }
            }
            var C_C = service.NumberOfTrips('C', 'C').Where(x => x.NumberOfStops == 2 || x.NumberOfStops == 3).Count();
            var A_C = service.NumberOfTrips('A', 'C').Where(x => x.NumberOfStops == 4).Count();

            Console.WriteLine($"Output #6: { C_C }");
            Console.WriteLine($"Output #7: { A_C }");

            Console.WriteLine($"Output #8: { service.ShortestDistance('A', 'C') }");
            Console.WriteLine($"Output #9: { service.ShortestDistance('B', 'B') }");


            var n = service.NumberOfTrips('C', 'C');
        }
    }
}
