using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class CircleShape : ClosedShape 
    {
        private double _diameter;

        public double Diameter
        {
            get => _diameter;
            set => _diameter = value > 0 ? value : 0;
        }

       
        public CircleShape() : base()
        {
            Diameter = 0;
        }

        public CircleShape(double x, double y, double diameter)
            : base(x, y)
        {
            Diameter = diameter;
        }

        public CircleShape(double x, double y, double diameter,
                          Brush stroke, Brush fill, double thickness = 2.0)
            : base(x, y, stroke, fill, thickness)
        {
            Diameter = diameter;
        }

        public override void Draw(Canvas canvas)
        {
            var ellipse = new Ellipse
            {
                Width = _diameter,     
                Height = _diameter,   
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(ellipse, X);
            Canvas.SetTop(ellipse, Y);
            canvas.Children.Add(ellipse);
        }
    }
}