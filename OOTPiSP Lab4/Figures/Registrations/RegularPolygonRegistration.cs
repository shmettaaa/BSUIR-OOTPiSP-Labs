using Figures.Factories;
using Figures.Renderers;

namespace Figures
{
    public class RegularPolygonRegistration : IFigureRegistration
    {
        public void Register()
        {
            FigureRegistry.Register(
                "Многоугольник",
                new FigureHandler(
                    new RegularPolygonFactory(),
                    new RegularPolygonRenderer()
                )
            );
        }
    }
}