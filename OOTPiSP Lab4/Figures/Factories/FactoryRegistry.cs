using System.Collections.Generic;

namespace Figures.Factories
{
    public static class FactoryRegistry
    {
        private static readonly Dictionary<string, IFigureFactory> _factories = new();

        static FactoryRegistry()
        {
            _factories.Add("Отрезок", new LineFactory());
            _factories.Add("Прямоугольник", new RectangleFactory());
            _factories.Add("Эллипс", new EllipseFactory());
            _factories.Add("Круг", new CircleFactory());
            _factories.Add("Треугольник", new TriangleFactory());
            _factories.Add("Многоугольник", new RegularPolygonFactory());
        }

        public static IFigureFactory? GetFactory(string name)
        {
            return _factories.TryGetValue(name, out var factory) ? factory : null;
        }

        public static IEnumerable<KeyValuePair<string, IFigureFactory>> GetAllFactories()
        {
            return _factories;
        }
    }
}