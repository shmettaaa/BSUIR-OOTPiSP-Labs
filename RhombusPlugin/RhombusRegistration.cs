using Figures;

namespace RhombusPlugin
{
    public class RhombusRegistration : IFigureRegistration
    {
        public void Register()
        {
            FigureRegistry.Register(
                "Ромб",                              
                new FigureHandler(
                    new RhombusFactory(),
                    new RhombusRenderer()
                )
            );
            ShapeSerializerRegistry.Register(typeof(RhombusShape), new RhombusShapeSerializer());
            // Also register renderer mapping so loaded RhombusShape instances can be rendered
            Figures.FigureRegistry.RegisterRenderer(typeof(RhombusShape), new RhombusRenderer());
        }
    }
}