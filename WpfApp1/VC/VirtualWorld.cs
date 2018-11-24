using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class VirtualWorld
    {
        public List<Wall3D> Walls { get; set; }
        public VirtualCamera VirtualCamera { get; set; }

        public VirtualWorld()
        {
            Walls = new List<Wall3D>();
            this.VirtualCamera = new VirtualCamera(new Point3D(0.0, 0.0, 0.0), 2.0, 2.0, 2.0);
        }

        public void AddElement(List<Wall3D> obj)
        {
            this.Walls.AddRange(obj);
        }
        public void Move(Direction direction)
        {
            foreach (var obj in Walls)
            {
                obj.Move(direction);
            }
        }
        public void Rotate(Direction direction)
        {
            foreach (var obj in Walls)
            {
                obj.Rotate(direction);
            }
        }
        public void ZoomIn()
        {
            this.VirtualCamera.ZoomIn();
        }
        public void ZoomOut()
        {
            this.VirtualCamera.ZoomOut();
        }
        public List<Wall2D> Generate2D()
        {
            return this.VirtualCamera.Calculate(Walls);
        }
        public void DivideWalls(int divider)
        {
            var smallWalls = new List<Wall3D>();
            foreach (var wall in Walls)
            {
                smallWalls.AddRange(DivideWallSimple(wall, divider));
            }
            this.Walls = smallWalls;
        }

        private IEnumerable<Wall3D> DivideWallSimple(Wall3D wall, int divider)
        {
            List<Wall3D> walls = new List<Wall3D>();
            var x1 = (wall.B.X - wall.A.X)/divider;
            var y1 = (wall.B.Y - wall.A.Y)/ divider;
            var z1 = (wall.B.Z - wall.A.Z)/ divider;

            var x2 = (wall.D.X - wall.A.X)/ divider;
            var y2 = (wall.D.Y - wall.A.Y)/ divider;
            var z2 = (wall.D.Z - wall.A.Z)/ divider;

            for (int i = 0; i < divider; i++)
            {
                for (int j = 0; j < divider; j++)
                {

                    Point3D pointA = new Point3D(wall.A.X + j * x1 + i * x2, wall.A.Y + j * y1 + i * y2, wall.A.Z + j * z1 + i * z2);
                    Point3D pointB = new Point3D(wall.A.X + (j + 1) * x1 + i * x2, wall.A.Y + (j + 1) * y1 + i * y2, wall.A.Z + (j + 1) * z1 + i * z2);
                    Point3D pointC = new Point3D(wall.A.X + (j + 1) * x1 + (i + 1) * x2, wall.A.Y + (j + 1) * y1 + (i + 1) * y2, wall.A.Z + (j + 1) * z1 + (i + 1) * z2);
                    Point3D pointD = new Point3D(wall.A.X + j * x1 + (i + 1) * x2, wall.A.Y + j * y1 + (i + 1) * y2, wall.A.Z + j * z1 + (i + 1) * z2);
                    Wall3D newWall = new Wall3D(pointA, pointB, pointC, pointD);
                    walls.Add(newWall);
                }
            }
            return walls;

        }
    }
}
