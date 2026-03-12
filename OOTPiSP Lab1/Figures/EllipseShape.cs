using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class EllipseShape : ClosedShape
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public EllipseShape() : base()
        {
            Cx = Cy = Width = Height = 0;
        }

        public EllipseShape(double cx, double cy, double width, double height,
                            Brush stroke, Brush fill, double thickness = 2.0)
            : base(stroke, fill, thickness)
        {
            Cx = cx;
            Cy = cy;
            Width = width > 0 ? width : 0;
            Height = height > 0 ? height : 0;
        }

        public override void Draw(Canvas canvas)
        {
            var ellipse = new Ellipse
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(ellipse, Cx - Width / 2);
            Canvas.SetTop(ellipse, Cy - Height / 2);
            canvas.Children.Add(ellipse);
        }
    }
}