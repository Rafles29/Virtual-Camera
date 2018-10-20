using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class VirtualWorld
    {
        public List<Object3D> Objects { get; set; }
        public VirtualCamera VirtualCamera { get; set; }

        public VirtualWorld()
        {
            Objects = new List<Object3D>();
            this.VirtualCamera = new VirtualCamera(new Point3D(0.0, 0.0, 0.0), 2.0, 2.0, 2.0);
        }

        public void AddElement(Object3D obj)
        {
            this.Objects.Add(obj);
        }
        public void Move(Direction direction)
        {
            foreach (var obj in Objects)
            {
                obj.Move(direction);
            }
        }
        public void Rotate(Direction direction)
        {
            foreach (var obj in Objects)
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
        public List<Line2D> Generate2D()
        {
            return this.VirtualCamera.Calculate(Objects);
        }
    }
}
