using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public enum Direction
    {
        UP,DOWN,LEFT,RIGHT,FORWARD,BACKWARD
    }
    public abstract class Object3D
    {
        public abstract void Move(Direction direction);
        public abstract void Rotate(Direction direction);
        public abstract List<Line3D> GetLines();
    }
}
