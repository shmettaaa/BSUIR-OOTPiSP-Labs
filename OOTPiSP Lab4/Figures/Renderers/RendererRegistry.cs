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
            return _renderers.TryGetValue(shape.GetType(), out var renderer) ? renderer : null;
        }
    }
}