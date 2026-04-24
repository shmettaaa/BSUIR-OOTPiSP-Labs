using Figures.Factories;
using Figures.Renderers;

namespace Figures
{
    public class LineRegistration : IFigureRegistration
    {
        public void Register()
        {
            FigureRegistry.Register(
                "Отрезок",
                new FigureHandler(
                    new LineFactory(),
                    new LineRenderer()
                )
            );
            ShapeSerializerRegistry.Register(typeof(LineShape), new LineShapeSerializer());
        }
    }
}