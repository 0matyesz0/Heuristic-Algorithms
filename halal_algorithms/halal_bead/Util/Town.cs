using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace halal_bead.Util
{
    public class Town
    {
        public Point Position { get; set; }

        public Town(double x, double y)
        {
            this.Position = new Point(x, y);
        }

        public Town(Point position)
        {
            this.Position = position;
        }
    }
}
