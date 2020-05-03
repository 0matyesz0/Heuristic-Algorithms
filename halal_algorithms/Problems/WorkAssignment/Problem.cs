using Solvers.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.WorkAssignment
{
    public class Problem
    {
        public List<Person> Persons { get; set; }
        public int RequestedTime { get; set; }

        public Problem()
        {
            this.Persons = new List<Person>();
            this.RequestedTime = 0;
            this.SetupData();
        }

        private void SetupData()
        {
            Dictionary<double, double> data = IOHandler.ReadInSalaryData();
            foreach (var pair in data)
            {
                this.Persons.Add(new Person(pair.Key, pair.Value));
            }

            this.RequestedTime = IOHandler.ReadInWorkHours();
        }

        public double SumSalary(List<int> solution)
        {
            double sum = 0;
            for (int i = 0; i < solution.Count(); i++)
            {
                sum += solution[i] * this.Persons[i].Salary;
            }

            return sum;
        }

        public double AverageQuality(List<int> solution)
        {
            double sum = 0;
            for (int i = 0; i < solution.Count(); i++)
            {
                sum += solution[i] * this.Persons[i].Quality;
            }
            return sum / this.RequestedTime;
        }
    }
}
