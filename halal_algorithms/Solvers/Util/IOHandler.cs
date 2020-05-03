using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solvers.Util
{
    public static class IOHandler
    {
        public static List<List<double>> ReadInPopulation()
        {
            List<List<double>> values = new List<List<double>>();
            using (StreamReader reader = new StreamReader("FuncApproxPopulation.txt"))
            {
                while (!reader.EndOfStream)
                {
                    List<double> lineNumbers = new List<double>();
                    var line = reader.ReadLine();
                    var lineValues = line.Split(';');
                    foreach (var item in lineValues)
                    {
                        lineNumbers.Add(double.Parse(item));
                    }
                    values.Add(lineNumbers);
                }
            }

            return values;
        }

        public static List<Dictionary<double, double>> ReadInFunction()
        {
            List<Dictionary<double, double>> valuePairs = new List<Dictionary<double, double>>();
            using (StreamReader reader = new StreamReader("FuncAppr1.txt"))
            {
                while (!reader.EndOfStream)
                {
                    Dictionary<double, double> pair = new Dictionary<double, double>();
                    var line = reader.ReadLine();
                    var lineValues = line.Split(';');
                    pair.Add(double.Parse(lineValues[0]), double.Parse(lineValues[1]));
                    valuePairs.Add(pair);
                }
            }
            return valuePairs;
        }

        public static int ReadInWorkHours()
        {
            using (StreamReader reader = new StreamReader("Salary.txt"))
            {
                return int.Parse(reader.ReadLine());
            }
        }

        public static Dictionary<double, double> ReadInSalaryData()
        {
            double price = 0;
            double quality = 0;
            Dictionary<double, double> priceQuality = new Dictionary<double, double>();
            using (StreamReader reader = new StreamReader("Salary.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var lineValues = line.Split(';');
                    price = double.Parse(lineValues[0]);
                    quality = double.Parse(lineValues[1]);
                    priceQuality.Add(price, quality);
                }
            }

            return priceQuality;
        }
    }
}
