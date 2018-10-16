using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class CohenSutherland
    {
        public CohenSutherland(Point2D middlePoint, double width, double height)
        {
            MiddlePoint = middlePoint;
            Width = width;
            Height = height;
            left = MiddlePoint.X - this.Width / 2;
            right = MiddlePoint.X + this.Width / 2;
            bottom = MiddlePoint.Y - this.Height / 2;
            top = MiddlePoint.Y + this.Height / 2;
        }

        public Point2D MiddlePoint { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double left;
        public double right;
        public double bottom;
        public double top;

        public Line TrimLine(Line2D line)
        {
            return Cohen_Sutherland(line.A.X, line.A.Y, line.B.X, line.B.Y);
        }
        public List<Line> TrimLines(List<Line2D> lines)
        {
            List<Line> drawLines = new List<Line>();
            foreach (var line in lines)
            {
                Line drawLine = this.TrimLine(line);
                if(drawLine != null)
                    drawLines.Add(drawLine);
            }
            return drawLines;
        }
        private byte calcRegCode(double x, double y)
        {
            byte result = 0;

            if (x < left) result |= 0x1;
            if (x > right) result |= 0x2;
            if (y > top) result |= 0x4;
            if (y < bottom) result |= 0x8;

            return result;
        }

        private Line Cohen_Sutherland(double x1, double y1, double x2, double y2)
        {
            byte rcode1, rcode2, rcode;


            rcode1 = calcRegCode(x1, y1);
            rcode2 = calcRegCode(x2, y2);


            if ((rcode1 & rcode2) != 0)
            {

            }

            else if ((rcode1 | rcode2) == 0)
            {
                Line drawLine = new Line();
                drawLine.X1 = x1;
                drawLine.X2 = x2;
                drawLine.Y1 = y1;
                drawLine.Y2 = y2;
                return drawLine;
            }
            else
            {
                do
                {
                    double x = x1, y = y1;

                    if (rcode1 != 0)
                    {
                        rcode = rcode1;
                    }
                    else
                    {
                        rcode = rcode2;
                    }

                    if ((rcode & 0x1) != 0)
                    {
                        y = y1 + (y2 - y1) * (left - x1) / (x2 - x1);
                        x = left;
                    }
                    else if ((rcode & 0x2) != 0)
                    {
                        y = y1 + (y2 - y1) * (right - x1) / (x2 - x1);
                        x = right;
                    }
                    else if ((rcode & 0x4) != 0)
                    {
                        x = x1 + (x2 - x1) * (bottom - y1) / (y2 - y1);
                        y = bottom;
                    }
                    else if ((rcode & 0x8) != 0)
                    {
                        x = x1 + (x2 - x1) * (top - y1) / (y2 - y1);
                        y = top;
                    }

                    if (rcode == rcode1)
                    {

                    }
                    else
                    {

                    }
                } while ((rcode1 & rcode2) == 0 && (rcode1 | rcode2) != 0);

                if ((rcode1 | rcode2) == 0)
                {
                    Line drawLine = new Line();
                    drawLine.X1 = x1;
                    drawLine.X2 = x2;
                    drawLine.Y1 = y1;
                    drawLine.Y2 = y2;
                    return drawLine;
                }
                else
                {
                }
            }
            return null;
        }

    }
}
