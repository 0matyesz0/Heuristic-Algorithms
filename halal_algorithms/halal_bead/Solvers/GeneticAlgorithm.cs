using halal_bead.Problems;
using halal_bead.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace halal_bead.Solvers
{
    public class GeneticAlgorithm
    {
        private static Random rnd = new Random();
        
        private const int selectionRate = 200;
        private const int mutationRate = 25; //25
        private const int populationCount = 500; //500
        
        public double globalFitness = -1;

        public FunctionApproximation Problem { get; set; }
        public List<Chromosome> Population { get; set; }
        public List<Chromosome> Selection { get; set; }

        public GeneticAlgorithm()
        {
            this.Problem = new FunctionApproximation();
            this.InitializePopulation();
        }

        private void InitializePopulation()
        {
            this.Population = new List<Chromosome>();
            this.Selection = new List<Chromosome>();

            for (int i = 0; i < populationCount; i++)
            {
                this.Population.Add(new Chromosome());
            }

            for (int i = 0; i < selectionRate; i++)
            {
                this.Selection.Add(new Chromosome());
            }
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
            foreach (var pair in this.Problem.TargetFunc)
            {
                double x = pair.Keys.FirstOrDefault();
                double targetY = pair.Values.FirstOrDefault();
                double y = (parameters[0] * Math.Pow(x - parameters[1], 3)) + (parameters[2] * Math.Pow(x - parameters[3], 2)) + parameters[4];
                
                double difference = (double)Math.Abs(y - targetY);
                sumDifference += difference;
            }

            return sumDifference;
        }

        public Chromosome GetBestChromosome()
        {
            this.Selection = this.Selection.OrderBy(o => CalculateFitness(o.parameters)).ToList();
            this.globalFitness = CalculateFitness(this.Selection[0].parameters);
            return this.Selection[0];
        }
    }
}
