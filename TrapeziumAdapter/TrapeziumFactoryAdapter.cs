using Figures;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace TrapeziumAdapter
{
    public class TrapeziumFactoryAdapter : IFigureFactory
    {
        private readonly Type _trapeziumType;

        public int RequiredPointCount => 4;

        public TrapeziumFactoryAdapter(Type trapeziumType)
        {
            _trapeziumType = trapeziumType;
        }

        public Shape CreateFromPoints(IReadOnlyList<Point> points, Brush stroke, Brush fill, double thickness, int sides = 0)
        {
            // Create Trapezium instance
            var trapezium = System.Activator.CreateInstance(_trapeziumType);

            // Convert points to int[]
            int[] coords = new int[points.Count * 2];
            for (int i = 0; i < points.Count; i++)
            {
                coords[i * 2] = (int)points[i].X;
                coords[i * 2 + 1] = (int)points[i].Y;
            }

            // Set private _point field
            var pointField = _trapeziumType.GetField("_point", BindingFlags.NonPublic | BindingFlags.Instance);
            pointField?.SetValue(trapezium, coords);

            // Return wrapper
            return new TrapeziumShapeWrapper(trapezium, stroke, fill, thickness);
        }
    }
}