using halal_bead.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace halal_bead.Problems
{
    public class TravelingAgent
    {
        public List<Town> Route { get; set; }

        public TravelingAgent()
        {
            this.Route = new List<Town>();
            this.SetupData();
        }

        private void SetupData()
        {
            List<List<int>> coordinates = IOHandler.ReadInCoordinates();
            for (int i = 0; i < coordinates.Count(); i++)
            {
                this.Route.Add(new Town(coordinates[i].FirstOrDefault(), coordinates[i].LastOrDefault()));
            }
        }

        public double CalculateDistanceOfTowns(Town t1, Town t2)
        {
            var t1Position = t1.Position;
            var t2Position = t2.Position;
            return Math.Sqrt(Math.Pow(t2Position.Y - t1Position.Y, 2) + Math.Pow((t2Position.X - t1Position.X), 2));
        }

        public double CalculateRouteDistance(List<Town> route)
        {
            double sumDistance = 0;
            for (int i = 0; i < route.Count() - 1; i++)
            {
                for (int j = 1; j < route.Count(); j++)
                {
                    sumDistance += this.CalculateDistanceOfTowns(route[i], route[j]);
                }
            }

            return sumDistance;
        }
    }
}
