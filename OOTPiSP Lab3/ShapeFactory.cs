using System;
using System.Collections.Generic;

namespace Figures
{
    public static class ShapeFactory
    {
        private static readonly Dictionary<string, Func<Shape>> _creators = new Dictionary<string, Func<Shape>>();

        static ShapeFactory()
        {
            RegisterAllShapes();
        }

        public static void RegisterShape(string className, Func<Shape> creator)
        {
            if (!_creators.ContainsKey(className))
                _creators.Add(className, creator);
        }

        public static void RegisterAllShapes()
        {
            RegisterShape("LineShape", () => new LineShape());
            RegisterShape("RectangleShape", () => new RectangleShape());
            RegisterShape("EllipseShape", () => new EllipseShape());
            RegisterShape("CircleShape", () => new CircleShape());
            RegisterShape("TriangleShape", () => new TriangleShape());
            RegisterShape("RegularPolygon", () => new RegularPolygon());
        }

        public static Shape CreateShape(string className)
        {
            if (_creators.TryGetValue(className, out Func<Shape> creator))
                return creator();

            throw new ArgumentException($"Unknown shape type: {className}");
        }

        public static bool IsShapeRegistered(string className) => _creators.ContainsKey(className);
        public static IEnumerable<string> GetRegisteredShapeNames() => _creators.Keys;
    }
}