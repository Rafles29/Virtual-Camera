using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Point3D : Object3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    this.Y = this.Y + 1;
                    break;
                case Direction.DOWN:
                    this.Y = this.Y - 1;
                    break;
                case Direction.LEFT:
                    this.X = this.X - 1;
                    break;
                case Direction.RIGHT:
                    this.X = this.X + 1;
                    break;
                case Direction.FORWARD:
                    this.Z = this.Z + 1;
                    break;
                case Direction.BACKWARD:
                    this.Z = this.Z - 1;
                    break;
                default:
                    break;
            }
        }

        public override void Rotate(Direction direction)
        {
            double angle = 0.01;
            double x2, y2, z2;
            switch (direction)
            {
                case Direction.UP:
                    y2 = this.Y;
                    z2 = this.Z;
                    this.Y = y2 * Math.Cos(angle) - z2* Math.Sin(angle);
                    this.Z = y2 * Math.Sin(angle) + z2* Math.Cos(angle);
                    break;
                case Direction.DOWN:
                    y2 = this.Y;
                    z2 = this.Z;
                    this.Y = y2 * Math.Cos(-angle) - z2 * Math.Sin(-angle);
                    this.Z = y2 * Math.Sin(-angle) + z2 * Math.Cos(-angle);
                    break;
                case Direction.LEFT:
                    x2 = this.X;
                    z2 = this.Z;
                    this.X = x2 * Math.Cos(angle) + z2 * Math.Sin(angle);
                    this.Z = -1 * x2 * Math.Sin(angle) + z2 * Math.Cos(angle);
                    break;
                case Direction.RIGHT:
                    x2 = this.X;
                    z2 = this.Z;
                    this.X = x2 * Math.Cos(-angle) + z2 * Math.Sin(-angle);
                    this.Z = -1 * x2 * Math.Sin(-angle) + z2 * Math.Cos(-angle);
                    break;
                case Direction.FORWARD:
                    x2 = this.X;
                    y2 = this.Y;
                    this.X = x2 * Math.Cos(angle) - y2 * Math.Sin(angle);
                    this.Y = x2 * Math.Sin(angle) + y2 * Math.Cos(angle);
                    break;
                case Direction.BACKWARD:
                    x2 = this.X;
                    y2 = this.Y;
                    this.X = x2 * Math.Cos(-angle) - y2 * Math.Sin(-angle);
                    this.Y = x2 * Math.Sin(-angle) + y2 * Math.Cos(-angle);
                    break;
                default:
                    break;
            }
        }

        public override string ToString()
        {
            return this.X.ToString() + " " + this.Y.ToString() + " " + this.Z.ToString();
        }

        public override List<Line3D> GetLines()
        {
            Line3D line3D = new Line3D(this, this);
            List <Line3D> list = new List<Line3D>();
            list.Add(line3D);
            return list;
        }
    }
}
