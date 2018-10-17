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

            this.VirtualWorld = new VirtualWorld();

            Cube cube = new Cube(new Point3D(0.0, 0.0, 5.0), 2.0);
            stackpanel.Background = Brushes.Yellow;
            this.VirtualWorld.AddElement(cube);
            this.Draw(stackpanel);
        }
        public VirtualWorld VirtualWorld { get; set; }
 
        public void Draw(StackPanel stackPanel)
        {
            List<Line2D> lines2D = this.VirtualWorld.Generate2D();
            List<Line2D> scaledLines2D = this.Scale(stackPanel, lines2D);
            this.DrawLines(stackPanel, scaledLines2D);
        }
        private List<Line2D> Scale(StackPanel stackPanel, List<Line2D> lines)
        {
            var windowHeight = 1000;
            var windowWidth = 1000;

            var cameraHeight = this.VirtualWorld.VirtualCamera.Height;
            var cameraWidth = this.VirtualWorld.VirtualCamera.Width;

            double heightScale = windowHeight / cameraHeight;
            double widthScale = windowWidth / cameraWidth;

            List<Line2D> output = new List<Line2D>();

            foreach (var line in lines)
            {
                Point2D A = new Point2D(windowWidth / 2 + line.A.X * widthScale, windowHeight / 2 + line.A.Y * heightScale * -1);
                Point2D B = new Point2D(windowWidth / 2 + line.B.X * widthScale, windowHeight / 2 + line.B.Y * heightScale * -1);
                Line2D newLine = new Line2D(A,B);
                output.Add(newLine);
            }
            return output;
        }

        private void DrawLine(StackPanel stackPanel, Line2D line2D)
        {
            Line line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = line2D.A.X,
                X2 = line2D.B.X,
                Y1 = line2D.A.Y,
                Y2 = line2D.B.Y,
            };
            stackpanel.Children.Add(line);
        }
        private void DrawLines(StackPanel stackPanel, List<Line2D> lines)
        {
            foreach (var line in lines)
            {
                this.DrawLine(stackPanel, line);
            }
        }
    }
}
