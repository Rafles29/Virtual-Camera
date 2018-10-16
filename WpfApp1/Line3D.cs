using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Line3D : Object3D
    {
        public Point3D A { get; set; }
        public Point3D B { get; set; }

        public Line3D(Point3D a, Point3D b)
        {
            A = a;
            B = b;
        }

        public override void Move(Direction direction)
        {
            this.A.Move(direction);
            this.B.Move(direction);
        }

        public override string ToString()
        {
            return "Line: " + A.ToString() + " " + B.ToString();
        }

        public override List<Line3D> GetLines()
        {
            List<Line3D> list = new List<Line3D>();
            list.Add(this);
            return list;
        }
    }
}
