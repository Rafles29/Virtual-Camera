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

            StackPanel stackPanel = new StackPanel();
            this.Content = stackPanel;

            this.Objects3D = new List<Object3D>();
            this.VirtualCamera = new VirtualCamera(2.0);
            this.PerspectiveProjection = new PerspectiveProjection();
            this.CohenSutherland = new CohenSutherland(new Point2D(0.0, 0.0), 2.0, 2.0);
            Cube cbe = new Cube(new Point3D(0.0, 0.0, 5.0), 2.0);
            this.Objects3D.Add(cbe);
            this.Draw(stackPanel);
        }
        public List<Object3D> Objects3D { get; set; }
        public List<Line2D> Lines { get; set; }
        public List<Line> DrawLines { get; set; }
        public VirtualCamera VirtualCamera { get; set; }
        public PerspectiveProjection PerspectiveProjection { get; set; }
        public CohenSutherland CohenSutherland { get; set; }
        public void Draw(StackPanel stackPanel)
        {
            this.Calculate();
            this.Scale(stackPanel);
            this.DrawFrame(stackPanel);
        }
        private void DrawFrame(StackPanel myGrid)
        {
            foreach (var line in this.DrawLines)
            {
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.StrokeThickness = 2;
               // Console.WriteLine("Linijka:" + line.X1 + " " + line.Y1 + " " + line.X2 + " " + line.Y2);
                myGrid.Children.Add(line);
            }
        }
        private void Scale(StackPanel myGrid)
        {
            var windowHeight = 1000;
            var windowWidth = 1000;

            var cameraHeight = CohenSutherland.Height;
            var cameraWidth = CohenSutherland.Width;

            double heightScale = windowHeight / cameraHeight;
            double widthScale = windowWidth / cameraWidth;

            foreach (var line in this.DrawLines)
            {
                line.X1 = windowWidth/2 + line.X1 * widthScale;
                line.X2 = windowWidth/2 + line.X2 * widthScale;
                line.Y1 = windowHeight/2 + line.Y1 * heightScale *-1;
                line.Y2 = windowHeight/2 + line.Y2 * heightScale *-1;
            }
        }
        private void Calculate()
        {
            List<Line3D> tmpList = new List<Line3D>();
            foreach (var obj in this.Objects3D)
            {
                tmpList.AddRange(obj.GetLines());
            }
            this.Lines = this.PerspectiveProjection.Project(tmpList, this.VirtualCamera.d);
            List<Line> dLines = this.CohenSutherland.TrimLines(this.Lines);
            this.DrawLines = dLines;
        }
    }
}
