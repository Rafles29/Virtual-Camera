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

        public VirtualCamera(double d)
        {
            Observator = new Point3D(0, 0, 0);
            this.d = d;
        }
        public void ZoomOut()
        {
            this.d = d * 2.0;
        }
        public void ZoomIn()
        {
            this.d = d / 2.0;
        }
    }
}
