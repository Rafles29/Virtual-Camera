using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Cube 
    {

        public Cube(Point3D middle, double sideLength)
        {
            this.Walls = new List<Wall3D>();
            this.LowerBase(middle, sideLength);
            this.UpperBase(middle, sideLength);
            this.Sides(middle, sideLength);
        }

        public Cube(List<Wall3D> walls)
        {
            Walls = walls;
        }

        public List<Wall3D> Walls { get; set; }

        public void Move(Direction direction)
        {
            foreach (var line in this.Walls)
            {
                line.Move(direction);
            }
        }
        public void Rotate(Direction direction)
        {
            foreach (var line in this.Walls)
            {
                line.Rotate(direction);
            }
        }
        private void LowerBase(Point3D middle, double sideLength)
        {
            double half = sideLength / 2.0;

            Wall3D wall = new Wall3D(new Point3D(middle.X - half , middle.Y - half, middle.Z - half), new Point3D(middle.X - half, middle.Y - half, middle.Z + half), new Point3D(middle.X + half, middle.Y - half, middle.Z + half), new Point3D(middle.X + half, middle.Y - half, middle.Z - half));
            this.Walls.Add(wall);
        }
        private void UpperBase(Point3D middle, double sideLength)
        {
            double half = sideLength / 2.0;

            Wall3D wall = new Wall3D(new Point3D(middle.X - half, middle.Y + half, middle.Z - half), new Point3D(middle.X - half, middle.Y + half, middle.Z + half), new Point3D(middle.X + half, middle.Y + half, middle.Z + half), new Point3D(middle.X + half, middle.Y + half, middle.Z - half));
            this.Walls.Add(wall);

        }
        private void Sides(Point3D middle, double sideLength)
        {
            double half = sideLength / 2.0;

            Wall3D wall = new Wall3D(new Point3D(middle.X - half, middle.Y - half, middle.Z - half), new Point3D(middle.X - half, middle.Y - half, middle.Z + half), new Point3D(middle.X - half, middle.Y + half, middle.Z + half), new Point3D(middle.X - half, middle.Y + half, middle.Z - half));
            this.Walls.Add(wall);

            wall = new Wall3D(new Point3D(middle.X + half, middle.Y - half, middle.Z - half), new Point3D(middle.X + half, middle.Y - half, middle.Z + half), new Point3D(middle.X + half, middle.Y + half, middle.Z + half), new Point3D(middle.X + half, middle.Y + half, middle.Z - half));
            this.Walls.Add(wall);

            wall = new Wall3D(new Point3D(middle.X - half, middle.Y - half, middle.Z + half), new Point3D(middle.X + half, middle.Y - half, middle.Z + half), new Point3D(middle.X + half, middle.Y + half, middle.Z + half), new Point3D(middle.X - half, middle.Y + half, middle.Z + half));
            this.Walls.Add(wall);

            wall = new Wall3D(new Point3D(middle.X - half, middle.Y - half, middle.Z - half), new Point3D(middle.X + half, middle.Y - half, middle.Z - half), new Point3D(middle.X + half, middle.Y + half, middle.Z - half), new Point3D(middle.X - half, middle.Y + half, middle.Z - half));
            this.Walls.Add(wall);
        }

        public override string ToString()
        {
            string answer = "Cube:" + System.Environment.NewLine;
            foreach (var wall in this.Walls)
            {
                answer += wall.ToString() + System.Environment.NewLine;
            }
            return answer;
        }


    }
}
