using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace halal_bead.Util
{
    public class Chromosome
    {
        private static Random rnd = new Random();

        public List<double> parameters { get; set; }

        public Chromosome()
        {
            this.parameters = new List<double>()
            {
                (double)rnd.Next(-100, 100),
                (double)rnd.Next(-100, 100),
                (double)rnd.Next(-100, 100),
                (double)rnd.Next(-100, 100),
                (double)rnd.Next(-100, 100)
            };
        }

        public Chromosome(List<double> parameters)
        {
            this.parameters = parameters;
        }

        public static Chromosome Mutation(Chromosome individual, double mutationRate)
        {
            Chromosome newBorn = new Chromosome();
            for (int i = 0; i < newBorn.parameters.Count(); i++)
            {
                newBorn.parameters[i] = (double)rnd.Next(100) >= mutationRate ? individual.parameters[i] : (double)rnd.Next(-100, 100);
            }
            return newBorn;
        }

        // Single gene mutation
        public static Chromosome CrossOver(Chromosome c1, Chromosome c2)
        {
            Chromosome newBorn = new Chromosome();
            for (int i = 0; i < newBorn.parameters.Count(); i++)
            {
                newBorn.parameters[i] = rnd.Next(101) >= 50 ? c1.parameters[i] : c2.parameters[i];
            }

            return newBorn;
        }
    }
}
