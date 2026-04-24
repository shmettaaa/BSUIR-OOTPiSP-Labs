using System;
using System.Collections.Generic;
using Figures.Renderers;

namespace Figures
{
    public static class RendererRegistry
    {
        private static readonly Dictionary<Type, IShapeRenderer> _renderers = new();

        static RendererRegistry()
        {
            _renderers.Add(typeof(LineShape), new LineRenderer());
            _renderers.Add(typeof(RectangleShape), new RectangleRenderer());
            _renderers.Add(typeof(EllipseShape), new EllipseRenderer());
            _renderers.Add(typeof(CircleShape), new CircleRenderer());
            _renderers.Add(typeof(TriangleShape), new TriangleRenderer());
            _renderers.Add(typeof(RegularPolygon), new RegularPolygonRenderer());
        }

        public static IShapeRenderer GetRenderer(Shape shape)
        {
            if (shape == null) return null;
            if (_renderers.TryGetValue(shape.GetType(), out var renderer))
                return renderer;

            // Fallback: check if any external/plugin renderer was registered in FigureRegistry
            var ext = FigureRegistry.GetRegisteredRenderer(shape.GetType());
            if (ext != null)
            {
                // Cache for faster subsequent lookup
                _renderers[shape.GetType()] = ext;
                return ext;
            }

            return null;
        }

        public static void Register(Type shapeType, IShapeRenderer renderer)
        {
            if (shapeType == null || renderer == null) return;
            if (!_renderers.ContainsKey(shapeType))
                _renderers[shapeType] = renderer;
        }
    }
}
