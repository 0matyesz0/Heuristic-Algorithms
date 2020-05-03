using halal_bead.Problems;
using halal_bead.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace halal_bead.Solvers
{
    public class HillClimbingStochastic
    {
        private static Random rnd = new Random();
        private int Epsilon = 3;

        public List<Town> Solutions { get; set; }
        public double globalFitness = -1;
        public TravelingAgent Problem { get; set; }
        public List<Town> CurrentRoute { get; set; }
        public List<int> OptimalSolution { get; set; }
        public List<Town> Examinee { get; set; }
        public Town CurrentTown { get; set; }
        public bool StopCondition { get; set; }

        public HillClimbingStochastic()
        {
            Problem = new TravelingAgent();
            this.StopCondition = false;

            // get the startpoint.
            this.CurrentRoute = this.Problem.Route;
            this.globalFitness = this.CalculateFitness(this.CurrentRoute);

            this.Examinee = new List<Town>();
            this.OptimalSolution = new List<int>();
            this.CurrentTown = this.CurrentRoute[rnd.Next(this.CurrentRoute.Count())];
        }

        // It gets stuck really quickly to a local optimum,
        // so i tried to modify it to SteepestAscent's stuck condition technique.
        private int iterationCount = 0;
        public void Repeat()
        {
            if (iterationCount % 100 != 0 && iterationCount < 500 && this.globalFitness > 20000)
            {
                List<Town> neighbours = this.GetNeighbours(this.CurrentTown);
                Town choosenNeighbour = neighbours[rnd.Next(neighbours.Count())];

                this.Examinee = this.ShuffleRoute(this.CurrentTown, choosenNeighbour);

                if (this.CalculateFitness(this.Examinee) < this.CalculateFitness(this.CurrentRoute))
                {
                    this.CurrentRoute = this.Examinee;
                    this.globalFitness = CalculateFitness(this.CurrentRoute);
                    this.CurrentTown = choosenNeighbour;
                }
                iterationCount++;
            }
            else if (iterationCount % 100 == 0 && this.globalFitness > 20000) // Got Stuck:
            {
                this.CurrentTown = this.CurrentRoute[rnd.Next(this.CurrentRoute.Count())];
                iterationCount++;
            }
            else // STOPCONDITION(max. 500 iteration) Reached:
            {
                this.StopCondition = true;
            }
                
        }

        private List<Town> GetNeighbours(Town current)
        {
            int index = this.CurrentRoute.IndexOf(current);
            List<Town> neighbours = new List<Town>();
            for (int i = index - this.Epsilon; i < index + this.Epsilon; i++)
            {
                if (this.CurrentRoute.ElementAtOrDefault(i) != null)
                {
                    neighbours.Add(this.CurrentRoute[i]);
                }
            }

            return neighbours;
        }

        private List<Town> ShuffleRoute(Town current, Town replacement)
        {
            List<Town> shuffledRoute = new List<Town>();

            foreach (Town town in this.CurrentRoute)
            {
                shuffledRoute.Add(town);
            }

            int currentIndex = this.CurrentRoute.IndexOf(current);
            int replacementIndex = this.CurrentRoute.IndexOf(replacement);

            shuffledRoute[currentIndex] = replacement;
            shuffledRoute[replacementIndex] = current;

            return shuffledRoute;
        }

        private double CalculateFitness(List<Town> route)
        {
            return this.Problem.CalculateRouteDistance(route);
        }
    }
}
