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
            Figures.FigureRegistry.RegisterRenderer(typeof(RhombusShape), new RhombusRenderer());
        }
    }
}