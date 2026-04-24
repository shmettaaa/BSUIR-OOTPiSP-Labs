using Figures.Factories;
using Figures.Renderers;

namespace Figures
{
    public class CircleRegistration : IFigureRegistration
    {
        public void Register()
        {
            FigureRegistry.Register(
                "Круг",
                new FigureHandler(
                    new CircleFactory(),
                    new CircleRenderer()
                )
            );
            ShapeSerializerRegistry.Register(typeof(CircleShape), new CircleShapeSerializer());
        }
    }
}