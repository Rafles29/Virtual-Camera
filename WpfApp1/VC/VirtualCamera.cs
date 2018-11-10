using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class VirtualCamera
    {
        public Point3D Observator { get; set; }
        public double d { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public PerspectiveProjection PerspectiveProjection { get; private set; }
        public CohenSutherland CohenSutherland { get; private set; }


        public VirtualCamera(Point3D observator, double d, double width, double height)
        {
            Observator = observator;
            this.d = d;
            Width = width;
            Height = height;
            this.PerspectiveProjection = new PerspectiveProjection();
            this.CohenSutherland = new CohenSutherland(new Point2D(0, 0), this.Width, this.Height);
        }

        public void ZoomOut()
        {
            this.d = d * 2.0;
        }
        public void ZoomIn()
        {
            this.d = d / 2.0;
        }

        public List<Line2D> Calculate(List<Object3D> object3Ds)
        {
            List<Line3D> lineList = new List<Line3D>();
            foreach (var obj in object3Ds)
            {
                lineList.AddRange(obj.GetLines());
            }
            List<Line2D> lineList2D = this.PerspectiveProjection.Project(lineList, this.d);
            List<Line2D> visibleLines2D = this.CohenSutherland.TrimLines(lineList2D);
            return visibleLines2D;
        }
        public List<Wall2D> Calculate(List<Wall3D> walls)
        {
            walls.Sort(delegate(Wall3D x, Wall3D y)
            {
                return y.Distance(this.Observator).CompareTo(x.Distance(this.Observator));
            });
            List<Wall2D> wall2Ds = this.PerspectiveProjection.Project(walls, this.d);
            return wall2Ds;
        }
    }
}
