using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class PerspectiveProjection
    {

        public Point2D Project(Point3D pointD, double d)
        {
            return new Point2D(pointD.X * d / pointD.Z, pointD.Y * d / pointD.Z);
        }

        public Line2D Project(Line3D line, double d)
        {
            return new Line2D(this.Project(line.A, d), this.Project(line.B, d));
        }

        public Wall2D Project(Wall3D wall, double d)
        {
            return new Wall2D(this.Project(wall.A, d), this.Project(wall.B, d), this.Project(wall.C, d), this.Project(wall.D, d));
        }

        public List<Line2D> Project(List<Line3D> line3Ds, double d)
        {
            List<Line2D> line2Ds = new List<Line2D>();
            foreach (var line in line3Ds)
            {
                line2Ds.Add(this.Project(line, d));
            }
            return line2Ds;
        }

        public List<Wall2D> Project(List<Wall3D> walls, double d)
        {
            List<Wall2D> walls2ds = new List<Wall2D>();
            foreach (var wall in walls)
            {
                walls2ds.Add(this.Project(wall, d));
            }
            return walls2ds;
        }
    }
}
