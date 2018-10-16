using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Line2D
    {
        public Line2D(Point2D a, Point2D b)
        {
            A = a;
            B = b;
        }

        public Point2D A { get; set; }
        public Point2D B { get; set; }

        public override string ToString()
        {
            return "Line: " + A.ToString() + " " + B.ToString();
        }
    }
}
