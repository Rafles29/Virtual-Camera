using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.WorldInit();
            this.Draw(canvas);
        }
        public VirtualWorld VirtualWorld { get; set; }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {

                case Key.Left:
                    Console.WriteLine("left");
                    this.VirtualWorld.Rotate(Direction.LEFT);
                    this.Draw(canvas);
                    break;
                case Key.Up:
                    Console.WriteLine("up");
                    this.VirtualWorld.Rotate(Direction.UP);
                    this.Draw(canvas);
                    break;
                case Key.Right:
                    Console.WriteLine("right");
                    this.VirtualWorld.Rotate(Direction.RIGHT);
                    this.Draw(canvas);
                    break;
                case Key.Down:
                    Console.WriteLine("down");
                    this.VirtualWorld.Rotate(Direction.DOWN);
                    this.Draw(canvas);
                    break;
                case Key.O:
                    Console.WriteLine("o");
                    this.VirtualWorld.Rotate(Direction.FORWARD);
                    this.Draw(canvas);
                    break;
                case Key.P:
                    Console.WriteLine("p");
                    this.VirtualWorld.Rotate(Direction.BACKWARD);
                    this.Draw(canvas);
                    break;

                case Key.W:
                    Console.WriteLine("w");
                    this.VirtualWorld.Move(Direction.BACKWARD);
                    this.Draw(canvas);
                    break;
                case Key.S:
                    Console.WriteLine("s");
                    this.VirtualWorld.Move(Direction.FORWARD);
                    this.Draw(canvas);
                    break;
                case Key.A:
                    Console.WriteLine("a");
                    this.VirtualWorld.Move(Direction.RIGHT);
                    this.Draw(canvas);
                    break;
                case Key.D:
                    Console.WriteLine("d");
                    this.VirtualWorld.Move(Direction.LEFT);
                    this.Draw(canvas);
                    break;
                case Key.LeftShift:
                    Console.WriteLine("shift");
                    this.VirtualWorld.Move(Direction.DOWN);
                    this.Draw(canvas);
                    break;
                case Key.LeftCtrl:
                    Console.WriteLine("ctrl");
                    this.VirtualWorld.Move(Direction.UP);
                    this.Draw(canvas);
                    break;

                case Key.Q:
                    Console.WriteLine("q");
                    this.VirtualWorld.ZoomIn();
                    this.Draw(canvas);
                    break;
                case Key.E:
                    Console.WriteLine("e");
                    this.VirtualWorld.ZoomOut();
                    this.Draw(canvas);
                    break;
                default:
                    break;
            }
        }

        public void WorldInit()
        {
            this.VirtualWorld = new VirtualWorld();
            Cube cube = new Cube(new Point3D(2.0, -2.0, 10.0), 3.0);
            Cube cube2 = new Cube(new Point3D(-2.0, -2.0, 10.0), 3.0);
            Cube cube3 = new Cube(new Point3D(2.0, 2.0, 10.0), 3.0);
            Cube cube4 = new Cube(new Point3D(-2.0, 2.0, 10.0), 3.0);
            Cube cube5 = new Cube(new Point3D(2.0, -2.0, 14.0), 3.0);
            Cube cube6 = new Cube(new Point3D(-2.0, -2.0, 14.0), 3.0);
            Cube cube7 = new Cube(new Point3D(2.0, 2.0, 14.0), 3.0);
            Cube cube8 = new Cube(new Point3D(-2.0, 2.0, 14.0), 3.0);

            this.VirtualWorld.AddElement(cube.Walls);
            this.VirtualWorld.AddElement(cube2.Walls);
            this.VirtualWorld.AddElement(cube3.Walls);
            this.VirtualWorld.AddElement(cube4.Walls);
            this.VirtualWorld.AddElement(cube5.Walls);
            this.VirtualWorld.AddElement(cube6.Walls);
            this.VirtualWorld.AddElement(cube7.Walls);
            this.VirtualWorld.AddElement(cube8.Walls);

            this.VirtualWorld.DivideWalls(4);

        }
        public void ClearFrame(Canvas canvas)
        {
            this.canvas.Children.Clear();
        }

        public void Draw(Canvas canvas)
        {
            this.canvas.Children.Clear();
            List<Wall2D> walls2D = this.VirtualWorld.Generate2D();
            List<Wall2D> scaledWalls2D = this.Scale(canvas, walls2D);
            this.DrawPolygons(canvas, scaledWalls2D);
        }
        private List<Wall2D> Scale(Canvas canvas, List<Wall2D> walls)
        {
            var windowHeight = canvas.Height;
            var windowWidth = canvas.Width;

            var cameraHeight = this.VirtualWorld.VirtualCamera.Height;
            var cameraWidth = this.VirtualWorld.VirtualCamera.Width;

            double heightScale = windowHeight / cameraHeight;
            double widthScale = windowWidth / cameraWidth;

            List<Wall2D> output = new List<Wall2D>();

            foreach (var wall in walls)
            {
                Point2D A = new Point2D(windowWidth / 2 + wall.A.X * widthScale, windowHeight / 2 - wall.A.Y * heightScale);
                Point2D B = new Point2D(windowWidth / 2 + wall.B.X * widthScale, windowHeight / 2 - wall.B.Y * heightScale);
                Point2D C = new Point2D(windowWidth / 2 + wall.C.X * widthScale, windowHeight / 2 - wall.C.Y * heightScale);
                Point2D D = new Point2D(windowWidth / 2 + wall.D.X * widthScale, windowHeight / 2 - wall.D.Y * heightScale);
                Wall2D newWall = new Wall2D(A,B,C,D);
                output.Add(newWall);
            }
            return output;
        }

        private void DrawPolygon(Canvas canvas, Wall2D wall)
        {
            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(new Point(wall.A.X, wall.A.Y));
            myPointCollection.Add(new Point(wall.B.X, wall.B.Y));
            myPointCollection.Add(new Point(wall.C.X, wall.C.Y));
            myPointCollection.Add(new Point(wall.D.X, wall.D.Y));

            Polygon myPolygon = new Polygon();
            myPolygon.Points = myPointCollection;
            myPolygon.Fill = Brushes.White;
            myPolygon.Stroke = Brushes.Black;
            myPolygon.StrokeThickness = 2;

            canvas.Children.Add(myPolygon);
            
        }
        private void DrawPolygons(Canvas canvas, List<Wall2D> walls)
        {
            foreach (var wall in walls)
            {
                this.DrawPolygon(canvas, wall);
            }
        }
    }
}
