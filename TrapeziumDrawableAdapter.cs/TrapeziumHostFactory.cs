using Figures;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using TrapeziumLibrary;

namespace FiguresApp.Adapters
{
    public class TrapeziumHostFactory : IFigureFactory
    {
        public int RequiredPointCount => 2;

        public Shape CreateFromPoints(
            IReadOnlyList<Point> points,
            Brush stroke,
            Brush fill,
            double thickness,
            int sides = 0)
        {
            if (points == null || points.Count < 2)
                return null;

            Point A = points[0];
            Point B = points[1];


            double left = System.Math.Min(A.X, B.X);
            double right = System.Math.Max(A.X, B.X);
            double topY = System.Math.Min(A.Y, B.Y);

            double width = right - left;
            if (width < 10) width = 50;

            double height = width * 0.5;

            double ax = left;
            double ay = topY;

            double bx = right;
            double by = topY;

            double cx = left - width * 0.25;   
            double cy = topY + height;

            double dx = right + width * 0.25;  
            double dy = topY + height;

            var shape = new Trapezium();

            shape.point[0] = (int)ax; 
            shape.point[1] = (int)ay;

            shape.point[2] = (int)bx; 
            shape.point[3] = (int)by;

            shape.point[4] = (int)dx; 
            shape.point[5] = (int)dy;

            shape.point[6] = (int)cx; 
            shape.point[7] = (int)cy;

            return new TrapeziumShapeWrapper(shape)
            {
                Stroke = stroke,
                StrokeThickness = thickness,
                Fill = fill
            };
        }
    }
}