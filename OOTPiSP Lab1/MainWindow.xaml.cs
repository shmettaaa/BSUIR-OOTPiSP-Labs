using System.Windows;
using System.Windows.Media;
using Figures;

namespace OOTPiSP_Lab1
{
    public partial class MainWindow : Window
    {
        private ShapeList _shapeList;

        public MainWindow()
        {
            InitializeComponent();
            InitializeShapes();
            _shapeList.DrawAll(DrawingCanvas);
        }

        private void InitializeShapes()
        {
            _shapeList = new ShapeList();

            _shapeList.Add(new LineShape(100, 100, 250, 150, Brushes.Red, 3));

            _shapeList.Add(new RectangleShape(100, 200, 150, 100,
                Brushes.Blue, Brushes.LightBlue, 2));

            _shapeList.Add(new EllipseShape(400, 250, 120, 80,
                Brushes.Green, Brushes.LightGreen, 2));

            _shapeList.Add(new CircleShape(600, 250, 50,
                Brushes.Orange, Brushes.Yellow, 3));

            _shapeList.Add(new TriangleShape(200, 400, 300, 500, 100, 500,
                Brushes.Purple, Brushes.Violet, 2));

            _shapeList.Add(new RegularPolygon(500, 450, 5, 70,
                Brushes.Brown, Brushes.SandyBrown, 2));

            _shapeList.Add(new RegularPolygon(700, 450, 6, 60,
                Brushes.DarkCyan, Brushes.LightCyan, 2));
        }
    }
}