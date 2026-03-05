using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class SquareShape : ClosedShape  
    {
        private double _side;

        public double Side
        {
            get => _side;
            set => _side = value > 0 ? value : 0;
        }

        public SquareShape() : base()
        {
            Side = 0;
        }

        public SquareShape(double x, double y, double side)
            : base(x, y)
        {
            Side = side;
        }

        public SquareShape(double x, double y, double side,
                          Brush stroke, Brush fill, double thickness = 2.0)
            : base(x, y, stroke, fill, thickness)
        {
            Side = side;
        }

        public override void Draw(Canvas canvas)
        {
            var rect = new Rectangle
            {
                Width = _side,      
                Height = _side,     
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
            canvas.Children.Add(rect);
        }
    }
}