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
