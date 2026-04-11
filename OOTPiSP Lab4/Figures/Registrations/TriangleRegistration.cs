using Figures.Factories;
using Figures.Renderers;

namespace Figures
{
    public class TriangleRegistration : IFigureRegistration
    {
        public void Register()
        {
            FigureRegistry.Register(
                "Треугольник",
                new FigureHandler(
                    new TriangleFactory(),
                    new TriangleRenderer()
                )
            );
        }
    }
}