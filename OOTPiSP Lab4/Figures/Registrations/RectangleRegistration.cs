using Figures;
using Figures.Factories;
using Figures.Renderers;

public class RectangleRegistration : IFigureRegistration
{
    public void Register()
    {
        FigureRegistry.Register(
            "Прямоугольник",
            new FigureHandler(
                new RectangleFactory(),
                new RectangleRenderer()
            )
        );
        ShapeSerializerRegistry.Register(typeof(RectangleShape), new RectangleShapeSerializer());
    }
}