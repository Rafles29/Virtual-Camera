using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Wall2D
    {
        public Wall2D(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public Point2D A { get; set; }
        public Point2D B { get; set; }
        public Point2D C { get; set; }
        public Point2D D { get; set; }

        public override string ToString()
        {
            return "Line: " + A.ToString() + " " + B.ToString() + " " + C.ToString() + " " + D.ToString();
        }
    }
}
