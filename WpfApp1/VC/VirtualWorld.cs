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
    }
}
