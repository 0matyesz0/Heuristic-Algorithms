using halal_bead.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace halal_bead.Solvers
{
    public class SimulatedAnnealing
    {
        private static Random rnd = new Random();

        public int tMax = 30; // max iteration
        public int maxTemperature = 50;
        private const double boltzmann = 1.3807;
        private int Epsilon = 3;
        private double temperature = 0;
        private double priceWeight = 0.7;
        private double qualityWeight = 0.3;

        public double globalFitness = -1;
        public int TimePassed { get; set; }
        public WorkAssignment Problem { get; set; }
        public List<int> Current { get; set; }
        public List<int> OptimalSolution { get; set; }
        public List<int> Examinee { get; set; }
        

        public SimulatedAnnealing()
        {
            Problem = new WorkAssignment();

            // get the startpoint.
            this.Current = this.Problem.Solutions[rnd.Next(this.Problem.Solutions.Count())];
            this.Examinee = new List<int>();
            this.OptimalSolution = this.Current;
        }

        private int iterationCount = 0;

        public void Repeat()
        {
            List<List<int>> neighbours = this.GetNeighbours();
            this.Examinee = neighbours[rnd.Next(neighbours.Count())];
            double energyDelta = this.CalculateFitness(this.Examinee) - this.CalculateFitness(this.Current);
            if(energyDelta < 0)
            {
                this.Current = this.Examinee;
                if(this.CalculateFitness(this.Current) < this.CalculateFitness(this.OptimalSolution))
                {
                    this.OptimalSolution = this.Current;
                    this.globalFitness = this.CalculateFitness(this.OptimalSolution);
                }
            }
            else
            {
                this.temperature = ModifyTemperature(this.iterationCount);
                double chance = (Math.Pow(Math.E, (-1 * (double)energyDelta / ((double)boltzmann * this.temperature))));
                if(rnd.Next(2) < chance)
                {
                    this.Current = this.Examinee;
                }
            }
            iterationCount++;
        }

        public List<List<int>> GetNeighbours()
        {
            int index = this.Problem.Solutions.IndexOf(this.Current);
            List<List<int>> neighbours = new List<List<int>>();
            for (int i = index - this.Epsilon; i < index + this.Epsilon; i++)
            {
                if(this.Problem.Solutions.ElementAtOrDefault(i) != null)
                {
                    neighbours.Add(this.Problem.Solutions[i]);
                }
            }

            return neighbours;
        }

        public double ModifyTemperature(int t)
        {
            return this.maxTemperature * Math.Pow(1 - (double)t / this.tMax, 2);
        }

        private double GetEnergyDifference(List<int> actual, List<int> examinee)
        {
            return this.CalculateFitness(examinee) - this.CalculateFitness(actual);
        }

        private double CalculateFitness(List<int> solution)
        {
            // Scalarizing:
            var avgQuality = this.Problem.AverageQuality(solution);
            var sumPrice = this.Problem.SumSalary(solution);
            return (qualityWeight * avgQuality) + (priceWeight * sumPrice);
        }
    }
}
