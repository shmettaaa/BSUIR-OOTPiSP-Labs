using Figures;
using Figures.Renderers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RhombusPlugin
{
    public class RhombusRenderer : IShapeRenderer
    {
        public void Render(Figures.Shape shape, Canvas canvas)
        {
            if (shape is RhombusShape rhombus)
            {
                double cx = rhombus.CenterX;
                double cy = rhombus.CenterY;
                double halfW = rhombus.Width / 2;
                double halfH = rhombus.Height / 2;

                PointCollection points = new PointCollection
                {
                    new Point(cx, cy - halfH), 
                    new Point(cx + halfW, cy), 
                    new Point(cx, cy + halfH), 
                    new Point(cx - halfW, cy)  
                };

                var polygon = new Polygon
                {
                    Points = points,
                    Stroke = rhombus.Stroke,
                    Fill = rhombus.Fill,
                    StrokeThickness = rhombus.StrokeThickness
                };
                canvas.Children.Add(polygon);
            }
        }
    }
}