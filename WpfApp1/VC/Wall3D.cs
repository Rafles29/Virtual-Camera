using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Wall3D
    {
        public Wall3D(Point3D a, Point3D b, Point3D c, Point3D d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public Point3D A { get; set; }
        public Point3D B { get; set; }
        public Point3D C { get; set; }
        public Point3D D { get; set; }
        public void Move(Direction direction)
        {
            this.A.Move(direction);
            this.B.Move(direction);
            this.C.Move(direction);
            this.D.Move(direction);
        }
        public void Rotate(Direction direction)
        {
            this.A.Rotate(direction);
            this.B.Rotate(direction);
            this.C.Rotate(direction);
            this.D.Rotate(direction);
        }
        public Point3D Center()
        {
            double x = (A.X + B.X + C.X + D.X) / 4;
            double y = (A.Y + B.Y + C.Y + D.Y) / 4;
            double z = (A.Z + B.Z + C.Z + D.Z) / 4;
            return new Point3D(x, y, z);
        }
        public double Distance(Point3D point)
        {
            Point3D center = this.Center();
            double distance = Math.Pow(center.X-point.X, 2) + Math.Pow(center.Y - point.Y, 2) + Math.Pow(center.Z - point.Z, 2);
            return distance;
        }

        public override string ToString()
        {
            return "WaLL: " + A.ToString() + " " + B.ToString() + " " + C.ToString() + " " + D.ToString();
        }
    }
}
