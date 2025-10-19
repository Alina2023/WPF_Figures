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

namespace Lab1_Figures
{
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        Triangle currentTriangle;
        Quadrilateral currentQuad;
        public MainWindow()
        {
            InitializeComponent();
        }
        // метод для отрисовки линии
        public void DrawLine(Point2D a, Point2D b, Brush color = null, double thickness = 2)
        {
            var line = new Line
            {
                Stroke = color ?? Brushes.Black,
                StrokeThickness = thickness,
                X1 = a.getX(),
                Y1 = a.getY(),
                X2 = b.getX(),
                Y2 = b.getY()
            };
            Scene.Children.Add(line);
        }
        public void DrawTriangle(Triangle tr)
        {
            DrawLine(tr.getP1(), tr.getP2(), Brushes.Red, 3);
            DrawLine(tr.getP2(), tr.getP3(), Brushes.Red, 3);
            DrawLine(tr.getP3(), tr.getP1(), Brushes.Red, 3);
        }
        public void DrawQuadrilateral(Quadrilateral q)
        {
            DrawLine(q.getP1(), q.getP2(), Brushes.Blue, 3);
            DrawLine(q.getP2(), q.getP3(), Brushes.Blue, 3);
            DrawLine(q.getP3(), q.getP4(), Brushes.Blue, 3);
            DrawLine(q.getP4(), q.getP1(), Brushes.Blue, 3);
        }
        private void BtnRandomTriangle_Click(object sender, RoutedEventArgs e)
        {
            Scene.Children.Clear();
            Point2D p1 = new Point2D(rnd.Next(0, (int)Scene.Width), rnd.Next(0, (int)Scene.Height));
            Point2D p2 = new Point2D(rnd.Next(0, (int)Scene.Width), rnd.Next(0, (int)Scene.Height));
            Point2D p3 = new Point2D(rnd.Next(0, (int)Scene.Width), rnd.Next(0, (int)Scene.Height));
            currentTriangle = new Triangle(p1, p2, p3);
            currentQuad = null;
            DrawTriangle(currentTriangle);
        }
        private void BtnRandomQuad_Click(object sender, RoutedEventArgs e)
        {
            Scene.Children.Clear();
            int w = rnd.Next(30, 200);
            int h = rnd.Next(30, 200);
            Point2D start = new Point2D(rnd.Next(0, (int)Math.Max(0, Scene.Width - w)), rnd.Next(0, (int)Math.Max(0, Scene.Height - h)));
            currentQuad = new Quadrilateral(start, w, h);
            currentTriangle = null;
            DrawQuadrilateral(currentQuad);
        }
        private void BtnSquare_Click(object sender, RoutedEventArgs e)
        {
            Scene.Children.Clear();
            Point2D start = new Point2D(50, 50);
            currentQuad = new Square(start, 100);
            currentTriangle = null;
            DrawQuadrilateral(currentQuad);
        }
        private void MoveLeft_Click(object sender, RoutedEventArgs e) => Move(-10, 0);
        private void MoveRight_Click(object sender, RoutedEventArgs e) => Move(10, 0);
        private void MoveUp_Click(object sender, RoutedEventArgs e) => Move(0, -10);
        private void MoveDown_Click(object sender, RoutedEventArgs e) => Move(0, 10);

        private void Move(int dx, int dy)
        {
            if (currentTriangle != null) { currentTriangle.addX(dx); currentTriangle.addY(dy); Redraw(); }
            if (currentQuad != null) { currentQuad.addX(dx); currentQuad.addY(dy); Redraw(); }
        }
        private void Redraw()
        {
            Scene.Children.Clear();
            if (currentTriangle != null) DrawTriangle(currentTriangle);
            if (currentQuad != null) DrawQuadrilateral(currentQuad);
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Scene.Children.Clear();
            currentTriangle = null;
            currentQuad = null;
        }
        private void BtnSaveScreenshot_Click(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)Scene.ActualWidth, (int)Scene.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            rtb.Render(Scene);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            var dlg = new Microsoft.Win32.SaveFileDialog { Filter = "PNG Image|*.png", FileName = "scene.png" };
            if (dlg.ShowDialog() == true)
            {
                using (var fs = System.IO.File.OpenWrite(dlg.FileName))
                    encoder.Save(fs);

                MessageBox.Show("Сохранено: " + dlg.FileName);
            }
        }
    }
}
