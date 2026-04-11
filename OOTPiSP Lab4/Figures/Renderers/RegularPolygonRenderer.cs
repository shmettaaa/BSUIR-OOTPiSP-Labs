using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Renderers
{
    public class RegularPolygonRenderer : IShapeRenderer
    {
        public void Render(Shape shape, Canvas canvas)
        {
            if (shape is RegularPolygon polygon)
            {
                Point[] points = new Point[polygon.Sides];
                for (int i = 0; i < polygon.Sides; i++)
                {
                    double angle = 2 * Math.PI * i / polygon.Sides - Math.PI / 2;
                    points[i] = new Point(
                        polygon.Cx + polygon.Radius * Math.Cos(angle),
                        polygon.Cy + polygon.Radius * Math.Sin(angle)
                    );
                }

                var wpfPolygon = new Polygon
                {
                    Points = new PointCollection(points),
                    Stroke = polygon.Stroke,
                    Fill = polygon.Fill,
                    StrokeThickness = polygon.StrokeThickness
                };
                canvas.Children.Add(wpfPolygon);
            }
        }
    }
}