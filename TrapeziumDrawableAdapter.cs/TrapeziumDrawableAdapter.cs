using Figures;
using Figures.Renderers;
using NewGraphicEditor.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TrapeziumLibrary;

namespace FiguresApp.Adapters
{
    public class TrapeziumDrawableAdapter : IShapeRenderer
    {
        private readonly IDrawableShape _pluginDrawer;

        public TrapeziumDrawableAdapter(IDrawableShape pluginDrawer)
        {
            _pluginDrawer = pluginDrawer;
        }

        public void Render(Figures.Shape shape, Canvas canvas)
        {
            if (shape == null || canvas == null)
                return;

            if (shape is not TrapeziumShapeWrapper wrapper)
                return;

            if (wrapper.InnerShape is not Trapezium pluginShape)
                return;

            int before = canvas.Children.Count;

            _pluginDrawer.Draw(canvas, pluginShape);

            Polygon? polygon = null;

            for (int i = before; i < canvas.Children.Count; i++)
            {
                if (canvas.Children[i] is Polygon p)
                {
                    polygon = p;
                    break;
                }
            }

            if (polygon == null)
                return;

            polygon.Fill = wrapper.Fill ?? Brushes.LightBlue;
            polygon.Stroke = wrapper.Stroke ?? Brushes.Black;
            polygon.StrokeThickness = wrapper.StrokeThickness;
        }
    }
}