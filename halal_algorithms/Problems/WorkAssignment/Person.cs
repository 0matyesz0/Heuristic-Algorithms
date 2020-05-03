using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.WorkAssignment
{
    public class Person
    {
        public double Salary { get; set; }
        public double Quality { get; set; }

        public Person(double salary, double quality)
        {
            this.Salary = salary;
            this.Quality = quality;
        }
    }
}
