using Figures.Factories;
using Figures.Renderers;

namespace Figures
{
    public class EllipseRegistration : IFigureRegistration
    {
        public void Register()
        {
            FigureRegistry.Register(
                "Эллипс",
                new FigureHandler(
                    new EllipseFactory(),
                    new EllipseRenderer()
                )
            );
            ShapeSerializerRegistry.Register(typeof(EllipseShape), new EllipseShapeSerializer());
        }
    }
}