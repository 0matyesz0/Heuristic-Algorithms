using Solvers.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solvers.Genetic
{
    public class GeneticAlgorithm
    {
        private static Random rnd = new Random();
        public List<Chromosome> Selection { get; set; }
        private const int selectionRate = 200;
        private const int mutationRate = 25; //25
        private const int populationCount = 500; //500
        public double globalFitness = -1;

        public List<Chromosome> Population { get; set; }
        public List<Dictionary<double, double>> TargetFunc { get; set; }
        public List<Dictionary<double, double>> ApproxFunc { get; set; }

        public GeneticAlgorithm()
        {
            this.InitializePopulation();
            this.InitializeFunctions();
        }

        private void InitializePopulation()
        {
            this.Population = new List<Chromosome>();
            this.Selection = new List<Chromosome>();

            // from file:
            //var IOdata = IOHandler.ReadInPopulation();
            //foreach (var list in IOdata)
            //{
            //    this.Population.Add(new Chromosome(list));
            //}
            for (int i = 0; i < populationCount; i++)
            {
                this.Population.Add(new Chromosome());
            }

            for (int i = 0; i < selectionRate; i++)
            {
                this.Selection.Add(new Chromosome());
            }
        }

        private void InitializeFunctions()
        {
            this.TargetFunc = IOHandler.ReadInFunction();
            this.ApproxFunc = new List<Dictionary<double, double>>();
        }

        public void Crossing()
        {
            for (int i = 0; i < this.Population.Count(); i++)
            {
                int first = rnd.Next(this.Selection.Count());
                int second = rnd.Next(this.Selection.Count() - 1);
                if (second >= first)
                    ++second;
                this.Population[i] = Chromosome.CrossOver(this.Selection[first], this.Selection[second]);
            }
        }

        public void Mutate()
        {
            for (int i = 0; i < this.Population.Count(); i++)
            {
                if (rnd.Next(100) < mutationRate)
                {
                    this.Population[i] = Chromosome.Mutation(this.Population[i], mutationRate);
                }
            }
        }

        public void FitnessSelection()
        {
            //// Order popultaion by fitness:
            //this.Population = this.Population.OrderBy(x => CalculateFitness(x.parameters)).ToList();

            // Rank based selection: the closer it is to the start of the population, the higher chance it gets to be selected.
            #region Rank based selection
            //int sum = 0;
            //int rank = 0;

            //for (int i = 0; i < this.Population.Count(); i++)
            //{
            //    sum += i;
            //}
            //for (int i = 0; i < selectionRate; i++)
            //{
            //    for (int j = 1; j < this.Population.Count() + 1; j++)
            //    {
            //        rank = j / sum;
            //    }
            //    int chance = rnd.Next(rank);
            //    int limit = 0;
            //    for (int j = 0; j < this.Population.Count(); j++)
            //    {
            //        limit += rank;
            //        if (chance <= limit)
            //        {
            //            this.Selection.Add(this.Population[j]);

            //        }
            //    }
            //}
            #endregion

            // Roulette wheel selection:
            #region Roulette wheel selection
            //this.Population = this.Population.OrderByDescending(x => CalculateFitness(x.parameters)).ToList();
            //double fitnessSum = this.Population.Sum(x => CalculateFitness(x.parameters));

            //this.Selection[0] = this.Population[0];

            //for (int i = 1; i < selectionRate; i++)
            //{
            //    int givenFitness = rnd.Next(Convert.ToInt64(fitnessSum)); // <-- THE NUMBER IS TOO BIG TO GENERATE A RANDOM NUMBER WITH.
            //    int limit = 0;
            //    for (int j = 0; j < this.Population.Count(); j++)
            //    {
            //        limit += Convert.ToInt32(CalculateFitness(this.Population[j].parameters));
            //        if (givenFitness < limit)
            //        {
            //            this.Selection[i] = this.Population[j];
            //            break;
            //        }
            //    }
            //}
            #endregion

            // Elitism:
            this.Population = this.Population.OrderBy(x => CalculateFitness(x.parameters)).ToList();
            for (int i = 0; i < selectionRate; i++)
            {
                this.Selection[i] = this.Population[i];
            }
        }

        private double CalculateFitness(List<double> parameters)
        {
            double sumDifference = 0;
            foreach (var pair in this.TargetFunc)
            {
                double x = pair.Keys.FirstOrDefault();
                double targetY = pair.Values.FirstOrDefault();
                double y = (parameters[0] * Math.Pow(x - parameters[1], 3)) + (parameters[2] * Math.Pow(x - parameters[3], 2)) + parameters[4];
                //double difference = (double)Math.Pow(y - targetY, 2);
                double difference = (double)Math.Abs(y - targetY);
                sumDifference += difference;
            }

            return sumDifference;
        }

        public double getYValue(double x)
        {
            this.Selection = this.Selection.OrderBy(o => CalculateFitness(o.parameters)).ToList();
            this.globalFitness = CalculateFitness(this.Selection[0].parameters);
            double y = (this.Selection[0].parameters[0] * Math.Pow(x - this.Selection[0].parameters[1], 3)) + (this.Selection[0].parameters[2] * Math.Pow(x - this.Selection[0].parameters[3], 2)) + this.Selection[0].parameters[4];
            return y;
        }
    }
}
