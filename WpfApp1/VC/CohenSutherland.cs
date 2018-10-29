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

        public Line2D TrimLine(Line2D line)
        {
            return Cohen_Sutherland(line.A.X, line.A.Y, line.B.X, line.B.Y);
        }
        public List<Line2D> TrimLines(List<Line2D> lines)
        {
            List<Line2D> drawLines = new List<Line2D>();
            foreach (var line in lines)
            {
                Line2D drawLine = this.TrimLine(line);
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
            if (y < bottom) result |= 0x4;
            if (y > top) result |= 0x8;


            return result;
        }

        private Line2D Cohen_Sutherland(double x1, double y1, double x2, double y2)
        {
            byte rcode1, rcode2, rcode;


            rcode1 = calcRegCode(x1, y1);
            rcode2 = calcRegCode(x2, y2);


            if ((rcode1 & rcode2) != 0)
            {
                return null;
            }

            else if ((rcode1 | rcode2) == 0)
            {
                Point2D A = new Point2D(x1, y1);
                Point2D B = new Point2D(x2, y2);
                Line2D drawLine = new Line2D(A, B);
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
                        x1 = x;
                        y1 = y;
                        rcode1 = calcRegCode(x1, y1);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        rcode2 = calcRegCode(x2, y2);
                    }
                } while ((rcode1 & rcode2) == 0 && (rcode1 | rcode2) != 0);

                if ((rcode1 | rcode2) == 0)
                {
                    Point2D A = new Point2D(x1, y1);
                    Point2D B = new Point2D(x2, y2);
                    Line2D drawLine = new Line2D(A, B);
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
