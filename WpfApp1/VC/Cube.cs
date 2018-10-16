using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Cube : Object3D
    {
        public Cube(List<Line3D> lines)
        {
            Lines = lines;
        }

        public Cube(Point3D middle, double sideLength)
        {
            this.Lines = new List<Line3D>();
            this.LowerBase(middle, sideLength);
            this.UpperBase(middle, sideLength);
            this.Sides(middle, sideLength);
        }

        public List<Line3D> Lines { get; set; }

        public override void Move(Direction direction)
        {
            foreach (var line in this.Lines)
            {
                line.Move(direction);
            }
        }
        private void LowerBase(Point3D middle, double sideLength)
        {
            double half = sideLength / 2.0;

            Line3D line = new Line3D(new Point3D(middle.X-half,middle.Y-half,middle.Z-half), new Point3D(middle.X - half, middle.Y - half, middle.Z + half));
            this.Lines.Add(line);

            line = new Line3D(new Point3D(middle.X - half, middle.Y - half, middle.Z - half), new Point3D(middle.X + half, middle.Y - half, middle.Z - half));
            this.Lines.Add(line);

            line = new Line3D(new Point3D(middle.X + half, middle.Y - half, middle.Z + half), new Point3D(middle.X - half, middle.Y - half, middle.Z + half));
            this.Lines.Add(line);

            line = new Line3D(new Point3D(middle.X + half, middle.Y - half, middle.Z + half), new Point3D(middle.X + half, middle.Y - half, middle.Z - half));
            this.Lines.Add(line);
        }
        private void UpperBase(Point3D middle, double sideLength)
        {
            double half = sideLength / 2.0;

            Line3D line = new Line3D(new Point3D(middle.X - half, middle.Y + half, middle.Z - half), new Point3D(middle.X - half, middle.Y + half, middle.Z + half));
            this.Lines.Add(line);

            line = new Line3D(new Point3D(middle.X - half, middle.Y + half, middle.Z - half), new Point3D(middle.X + half, middle.Y + half, middle.Z - half));
            this.Lines.Add(line);

            line = new Line3D(new Point3D(middle.X + half, middle.Y + half, middle.Z + half), new Point3D(middle.X - half, middle.Y + half, middle.Z + half));
            this.Lines.Add(line);

            line = new Line3D(new Point3D(middle.X + half, middle.Y + half, middle.Z + half), new Point3D(middle.X + half, middle.Y + half, middle.Z - half));
            this.Lines.Add(line);

        }
        private void Sides(Point3D middle, double sideLength)
        {
            double half = sideLength / 2.0;

            Line3D line = new Line3D(new Point3D(middle.X - half, middle.Y + half, middle.Z - half), new Point3D(middle.X - half, middle.Y - half, middle.Z - half));
            this.Lines.Add(line);

            line = new Line3D(new Point3D(middle.X - half, middle.Y + half, middle.Z + half), new Point3D(middle.X - half, middle.Y - half, middle.Z + half));
            this.Lines.Add(line);

            line = new Line3D(new Point3D(middle.X + half, middle.Y + half, middle.Z - half), new Point3D(middle.X + half, middle.Y - half, middle.Z - half));
            this.Lines.Add(line);

            line = new Line3D(new Point3D(middle.X + half, middle.Y + half, middle.Z + half), new Point3D(middle.X + half, middle.Y - half, middle.Z + half));
            this.Lines.Add(line);

        }

        public override string ToString()
        {
            string answer = "Cube:" + System.Environment.NewLine;
            foreach (var line in this.Lines)
            {
                answer += line.ToString() + System.Environment.NewLine;
            }
            return answer;
        }

        public override List<Line3D> GetLines()
        {
            return this.Lines;
        }
    }
}
