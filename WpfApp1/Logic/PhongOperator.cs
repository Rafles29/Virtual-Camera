using System;
using System.Drawing;
using Grafika.Model;
using Grafika.Helpers;

namespace Grafika.Logic
{


    public class PhongOperator
    {
        // Źródło światła
        public Point3D Source = new Point3D(0,0,200);

        // Natężenie światła w otoczeniu obiektu (jednakowe dla wszystkich obiektów)
        public const double Ia = 100;

        // Natężenie światła punktowego
        public const double Ip = 60000;

        // Współczynnik odbicia światła otoczenia (tła)
        public const double Ka = 0.4;

        // Krok o ile przesuamy źrodło światła
        public const int Step = 10;

        public Bitmap PhongAlgorithm(Bitmap image, Surface material)
        {
        
            Bitmap newImage = new Bitmap(500, 500);
            var lockBitmap = new LockBitmap(image);
            lockBitmap.LockBits();
            var newLockBitmap = new LockBitmap(newImage);
            newLockBitmap.LockBits();

            for (var i = 0; i < 500; i++)
            {
                for (var j = 0; j < 500; j++)
                {
                    var pixelColor = lockBitmap.GetPixel(j,i);

                    if (pixelColor != System.Drawing.Color.Black)
                    {
                        var point = ComputeZ(i - 250, j - 250, 150);
                        var l = point.ToVector();
                        l.Normalize();
                        var n = ComputeVector(point, Source);
                        n.Normalize();

                        //Model odbicia Phonga
                        var I = CalculateLightReflection(material, Scalar(n, l), 
                                    CalculateCosAlpha(ComputeVector(Source, point), l), Fatt(point));

                        //Obliczanie nowych kolorów
                        var red = Check(pixelColor.R + I);
                        var green = Check(pixelColor.G + I);
                        var blue = Check(pixelColor.B + I);

                        newLockBitmap.SetPixel(j,i, System.Drawing.Color.FromArgb(red, green, blue));
                    }
                }
            }
            lockBitmap.UnlockBits();
            newLockBitmap.UnlockBits();

            return newImage;
        }

        public void MoveRight()
        {
            Source.Y = Source.Y + Step;
        }

        public void MoveLeft()
        {
            Source.Y = Source.Y - Step;
        }

        public void Forward()
        {
            Source.Z = Source.Z + Step;
        }

        public void Backward()
        {
            Source.Z = Source.Z - Step;
        }

        public void Up()
        {
            Source.Z = Source.Z + Step;
        }

        public void Down()
        {
            Source.Z = Source.Z - Step;

        }

        private static int Check(double i)
        {
            if (i < 0)
            {
                return 0;
            }
            else if (i > 255)
            {
                return 255;
            }
            else
            {
                return (int)i;
            }
        }

        private double CalculateLightReflection(Surface surface, double scalar, double cosAlpha, double Fatt)
        {
            return Ia * Ka
                    + Fatt * Ip * surface.Kd * scalar
                    + Fatt * Ip * surface.Ks * Math.Pow(cosAlpha, surface.N);
        }

        // wyliczenie punktu 3D znajac połeżenie na kuli
        private static Point3D ComputeZ(int x, int y, double r)
        {
            return new Point3D(x, y, (int)Math.Sqrt(r * r - x * x - y * y));
        }

        private static Vector ComputeVector(Point3D start, Point3D end)
        {
            return new Vector(end.X - start.X, end.Y - start.Y, end.Z - start.Z);
        }

        private static double Scalar(Vector v, Vector b)
        {
            return v.X * b.X + v.Y * b.Y + v.Z * b.Z;
        }

        // Funkcja cos^n(a) opisuje odbicie kierunkowe
        private static double CalculateCosAlpha(Vector v, Vector b)
        {
            var distance = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z) * Math.Sqrt(b.X * b.X + b.Y * b.Y + b.Z * b.Z);
            if (Scalar(v, b) > 0) return 0;
            return Scalar(v, b) / distance;
        }

        // Współczynnik tłumienia źródła światła z odległością
        private double Fatt(Point3D p)
        {
            var distance = Math.Pow(p.X + Source.X, 2) + Math.Pow(p.Y + Source.Y, 2) + Math.Pow(p.Z + Source.Z, 2);
            return 1.0 / Math.Sqrt(distance);
        }
    }
}
