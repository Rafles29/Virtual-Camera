using Grafika.Logic;
using Grafika.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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

        private  PhongOperator phongOperator;
        private  Surface surface;
        private  Bitmap shadedBitmap;

        public MainWindow()
        {
            InitializeComponent();
            this.WorldInit();
            this.Draw();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {

                case Key.Left:
                    Console.WriteLine("left");
                    phongOperator.MoveLeft();
                    break;
                case Key.Up:
                    Console.WriteLine("up");
                    phongOperator.Up();
                    break;
                case Key.Right:
                    Console.WriteLine("right");
                    phongOperator.MoveRight();
                    break;
                case Key.Down:
                    Console.WriteLine("down");
                    phongOperator.Down();
                    break;
                case Key.LeftShift:
                    Console.WriteLine("shift");
                    phongOperator.Forward();
                    break;
                case Key.LeftCtrl:
                    Console.WriteLine("ctrl");
                    phongOperator.Backward();
                    break;
                case Key.D1:
                    Console.WriteLine("mirror");
                    surface.Ks = 0.25;
                    surface.Kd = 0.75;
                    surface.N = 5;
                    break;
                case Key.D2:
                    Console.WriteLine("custom");
                    surface.Ks = 0.5;
                    surface.Kd = 0.5;
                    surface.N = 10;
                    break;
                case Key.D3:
                    Console.WriteLine("wood");
                    surface.Ks = 0.75;
                    surface.Kd = 0.25;
                    surface.N = 100;
                    break;

                default:
                    break;
            }
            this.Draw();
        }

        public void WorldInit()
        {
            shadedBitmap = new Bitmap(500, 500);
            Graphics g = Graphics.FromImage(shadedBitmap);
            g.Clear(System.Drawing.Color.Black);
            g.FillEllipse(new SolidBrush(System.Drawing.Color.DarkBlue), 100, 100, 300, 300);
            image.Source = ImageSourceForBitmap(shadedBitmap);

            phongOperator = new PhongOperator();
            surface = new Surface
            {
                Ks = 0.25,
                Kd = 0.75,
                N = 5
            };

        }

        public void Draw()
        {
            image.Source = ImageSourceForBitmap(phongOperator.PhongAlgorithm(shadedBitmap, surface));
        }

        //If you get 'dllimport unknown'-, then add 'using System.Runtime.InteropServices;'
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceForBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
    }
}
