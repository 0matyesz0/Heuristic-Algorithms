using halal_bead.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace halal_bead.Problems
{
    public class FunctionApproximation
    {
        public List<Dictionary<double, double>> TargetFunc { get; set; }
        public List<Dictionary<double, double>> ApproxFunc { get; set; }

        public FunctionApproximation()
        {
            this.TargetFunc = IOHandler.ReadInFunction();
            this.ApproxFunc = new List<Dictionary<double, double>>();
        }
    }
}
